import "./App.css";
import React, { useState } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import NavBar from "./NavBar";
import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";

interface ValidationModel {
  error: boolean;
  message: string;
}

function GetFractionDigitsCount(number: string) {
  const parts = number.toString().split(".");
  if (parts.length === 1) {
    // No decimal part
    return 0;
  } else {
    // Count the number of digits in the decimal part
    return parts[1].length;
  }
}

function App() {
  const [inputValue, setInputValue] = useState("");
  const [result, setResult] = useState("");
  const [message, setMessage] = useState("");

  const handleApiCall = async () => {
    setMessage("");
    if (parseFloat(inputValue) <= 0) {
      setMessage("The value shold be greater than zero");
    } else if (parseFloat(inputValue) > 999999999999999.99) {
      setMessage("The value it too big");
    } else if (GetFractionDigitsCount(inputValue) > 2) {
      setMessage("Fraction digits should not be more than 2 digits");
    } else {
      const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ Number: inputValue }),
      };
      const response = await fetch(
        `https://localhost:7261/Converter/ConvertNumberToWords`,
        requestOptions
      )
        .then((response) => response.text())
        .then((data) => setResult(data))
        .catch((error) => console.error(error));
    }
  };

  return (
    <div className="App">
      <NavBar></NavBar>

      <Box
        style={{
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          marginTop: "2%",
        }}
      >
        <Paper elevation={3} sx={{ width: 600, padding: 1 }}>
          <Box sx={{ width: 600, height: 270 }}>
            <Grid container>
              <Grid xs={9}>
                <TextField
                  label="Enter a decimal number:"
                  value={inputValue}
                  onChange={(e) => setInputValue(e.target.value)}
                  type="number"
                  variant="standard"
                  size="medium"
                  fullWidth
                />
              </Grid>

              <Grid xs={3}>
                <Button
                  variant="contained"
                  onClick={handleApiCall}
                  style={{ marginTop: "10px" }}
                >
                  Convert
                </Button>
              </Grid>
              <Grid xs={12} sx={{ height: 50 }}>
                <p style={{ color: "red" }}>{message}</p>
              </Grid>
              <Grid xs={12}>
                <TextField
                  id="outlined-multiline-static"
                  label="Result"
                  multiline
                  rows={4}
                  value={result}
                  fullWidth
                />
              </Grid>
            </Grid>
          </Box>
        </Paper>
      </Box>
    </div>
  );
}

export default App;
