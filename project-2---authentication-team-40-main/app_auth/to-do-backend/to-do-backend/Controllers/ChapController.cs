using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using to_do_backend.Dtos;
using to_do_backend.JWTConfiguration;
using to_do_backend.Models;
using to_do_backend.Utils;

namespace to_do_backend.Controllers
{
    [Route("api/chap")]
    [ApiController]
    public class ChapController : ControllerBase
    {
        private readonly BackendContext db;
        private readonly IJwtAuth jwtAuth;
        public ChapController(BackendContext backendContext, IJwtAuth auth)
        {
            db = backendContext;
            jwtAuth = auth;
        }

        IEnumerable<bool> GetBits(byte b)
        {
            for (int i = 0; i < 8; i++)
            {
                yield return (b & 0x80) != 0;
                b *= 2;
            }
        }

        private bool[] parseSecret(string secret)
        {
            var secret_bytes = Encoding.UTF8.GetBytes(secret);
            BitArray ba = new BitArray(secret_bytes);
            bool[] result = new bool[ba.Length];
            ba.CopyTo(result, 0);
            return result;

        }

        [HttpPost]
        [Route("challenge")]
        public ActionResult getChallenge(string username)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return NotFound();
            var challenge = new Challenge();
            challenge.UserId = user.Id;

            challenge.secret = Hasher.HashPassword(user.Password, challenge.ChallengeValue).Item2;
            var something = parseSecret(challenge.secret);
            db.Challenges.Add(challenge);
            db.SaveChanges();

            var reponse = new
            {
                challengeId = challenge.Id,
                challenge = Convert.ToBase64String(challenge.ChallengeValue),
            };

            return Ok(reponse);
        }

        [HttpPost]
        [Route("validate")]
        public ActionResult validate(int id, char answer, int c_index)
        {
            var challenge = db.Challenges.Find(id);
            if (challenge == null) return NotFound();

            var user = db.Users.Find(challenge.UserId);

            if(c_index >= challenge.secret.Length - 2)
            {
                if(challenge.IsFailed)
                {
                    return Unauthorized();
                }
                var token = jwtAuth.Authentication(user.Username, user.Password);
                if (token == null) return Unauthorized();
                return Ok(new { token });
            }

            var correct_answer = challenge.secret[c_index];
            var response = new ChapChallenge();
            response.id = challenge.Id;
            response.index = c_index + 1;
            if (correct_answer == answer && !challenge.IsFailed)
            {
                response.answer = challenge.secret[c_index + 1];
                return Ok(response);
            }
            else
            {
                challenge.IsFailed = true;
                response.answer = getRandomChar();
            }

            db.SaveChanges();
            return Ok(response);
        }

        private char getRandomChar()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return chars[random.Next(chars.Length)];
        }

        //[HttpPost]
        //[Route("validate")]
        //public ActionResult validate(int id, char answer, int c_index)
        //{
        //    var challenge = db.Challenges.Find(id);
        //    if (challenge == null) return NotFound();

        //    var user = db.Users.Find(challenge.UserId);
        //    if (user == null) return NotFound();

        //    var result = Encoder.encrypt_challenge(user.Password, Convert.ToBase64String(challenge.ChallengeValue));
        //    var length = result.Length;
        //    Random random = new Random();
        //    var index = random.Next(0, result.Length - 1);

        //    if (challenge.IsFailed)
        //    {
        //        var response = new ChapChallenge();
        //        if(challenge.IterationCount < 10)
        //        {
        //            challenge.IterationCount += 1;

        //            var nonsenseIndex = random.Next(0, result.Length - 1);
        //            response.index = index;
        //            response.answer = result[nonsenseIndex];
        //            response.id = id;

        //            db.SaveChanges();
        //            return Ok(response);
        //        }
        //        return Unauthorized();
        //    }

        //    if (challenge.IterationCount < 10)
        //    {
        //        var response = new ChapChallenge();
        //        if(answer == result[c_index])
        //        {
        //            challenge.IterationCount += 1;

        //            response.index = index;
        //            response.answer = result[index];
        //            response.id = id;

        //            db.SaveChanges();
        //            return Ok(response);
        //        }
        //        else
        //        {
        //            challenge.IterationCount += 1;
        //            challenge.IsFailed = true;

        //            var nonsenseIndex = random.Next(0, result.Length - 1);
        //            response.index = index;
        //            response.answer = result[nonsenseIndex];
        //            response.id = id;

        //            db.SaveChanges();
        //            return Ok(response);
        //        }
        //    }

        //    var token = jwtAuth.Authentication(user.Username, user.Password);
        //    if (token == null) return Unauthorized();

        //    ChapChallenge tokenResponse = new ChapChallenge();
        //    tokenResponse.token = token;
        //    return Ok(token);
        //}
    }
}
