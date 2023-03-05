import { Grid, Hidden } from "@mui/material";
import loginPhoto from "../../Assets/login-logo.svg";
import LoginForm from "../../Components/LoginForm/LoginForm";
import { useState } from "react";
import { authenticate, getCurrentUser } from "../../Api/PublicApi";
import { postChapAutherization } from "../../Api/ChapApi";

export default function LoginPage() {
  const [errorMessage, setErrorMessage] = useState(null);
  const onLogin = (username, password) => {
    setErrorMessage(null);
    authenticate(username, password)
      .then((tokenResponse) => {
        getCurrentUser(tokenResponse.data)
          .then((userResponse) => {
            const { data } = userResponse;
            localStorage.setItem("token", tokenResponse.data);
            localStorage.setItem("id", data.id);
            localStorage.setItem("role", data.role);
            localStorage.setItem("username", data.username);
            window.location.reload();
          })
          .catch(() => setErrorMessage("User not found or wrong credentials"));
      })
      .catch(() => setErrorMessage("User not found or wrong credentials"));
  };

  const onChapLogin = () => {
    setErrorMessage(null);
    postChapAutherization("https://localhost:44314/api/chap")
      .then((tokenResponse) => {
        getCurrentUser(tokenResponse.data)
          .then((userResponse) => {
            const { data } = userResponse;
            localStorage.setItem("token", tokenResponse.data);
            localStorage.setItem("id", data.id);
            localStorage.setItem("role", data.role);
            localStorage.setItem("username", data.username);
            window.location.reload();
          })
          .catch((error) => {
            if (error.response) {
              const { data, status } = error.response;
              if (status === 401)
                alert(
                  "Authentication failed. Unauthorized. Check your Server application"
                );
              else if (status === 400) {
                alert(data);
              } else if (status === 404) {
                alert("Invalid credentials provided in the server");
              } else if (status === 500)
                alert(
                  "Server Service failed unexpectedly. Please try again later"
                );
              else {
                alert("Unexpected error" + status + data);
              }
            } else if (error.request) {
              alert("Backend service is not running or not reachable");
            } else {
              console.log("Error");
            }
          });
      })
      .catch((error) => {
        if (error.response) {
          const { data, status } = error.response;
          if (status === 401)
            alert(
              "Authentication failed. Unauthorized. Check your UAP application"
            );
          else if (status === 400) {
            alert(data);
          } else if (status === 404) {
            alert("Invalid credentials provided in the UAP");
          } else if (status === 500)
            alert("UAP Service failed unexpectedly. Please try again later");
          else {
            alert("Unexpected error" + status + data);
          }
        } else if (error.request) {
          alert("Backend service is not running or not reachable");
        } else {
          console.log("Error");
        }
      });
  };

  return (
    <Grid className="page" container alignContent="center" spacing={4}>
      <Hidden mdDown>
        <Grid item md={6} lg={7}>
          <img id="workerImage" src={loginPhoto} alt="login" />
        </Grid>
      </Hidden>
      <Grid item sm={11} md={6} lg={5}>
        <LoginForm
          onSubmit={onLogin}
          errorMessage={errorMessage}
          onChapLogin={onChapLogin}
        />
      </Grid>
    </Grid>
  );
}
