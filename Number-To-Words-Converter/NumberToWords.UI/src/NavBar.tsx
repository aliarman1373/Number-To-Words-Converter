import { AppBar, Box, IconButton, Toolbar, Typography } from "@mui/material";

export default function NavBar() {
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar>
          <IconButton
            size="large"
            edge="start"
            color="inherit"
            aria-label="menu"
          ></IconButton>
          <Typography variant="h4" sx={{ flexGrow: 1.1 }}>
            Word To Number Converter
          </Typography>
        </Toolbar>
      </AppBar>
    </Box>
  );
}
