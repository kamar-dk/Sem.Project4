import React from 'react';
import { useState, useEffect } from 'react';
import { Parallax } from 'react-parallax';
import "../App.css";
import {
  Grid, Paper, Typography, Button,
  Select, FormControl, InputLabel, Box, TextField
} from "@material-ui/core";
import { lightBlue } from "@material-ui/core/colors";
import { OutlinedInput } from "@material-ui/core";



function User({}) {
  const [userData, setUserData] = useState({});
  const [loading,setLoading] = useState(true);

  useEffect(() => {
   const fetchData= async()=>{

    const email= localStorage.getItem("email");
    const url = `https://localhost:7221/api/UserDatas/${email}`;
    try {
      const response = await fetch(url, {
        method: 'GET',
        mode: 'cors',
        headers: {
          'Authorization': 'Bearer ' + localStorage.getItem("token"),
          'Content-Type': 'application/json'
        }
      });
      if (response.ok) {
        const data = await response.json();
        setUserData(data);
      } else {
        console.error('Error fetching user data:', response.statusText);
      }
    } catch (error) {
      console.error('Error fetching user data:', error);
    }

    setLoading(false);
  };
  fetchData();
  }, []);
 
  if (loading) {
    return <p>Loading user data...</p>;
  }

  if (!userData) {
    return <p>Error: User data not found.</p>;
  }

  const deleteUser = () => {
    // Implement your delete user functionality here
    const confirmed = window.confirm("Are you sure you want to delete your user account?");
    if (confirmed) {
      // Implement your delete user functionality here
      alert("Delete User");
      const userId = userData.id; // Assuming you have a user ID available
  
      fetch(`https://localhost:7221/api/Users/${userData.email}`, {
        method: 'DELETE',
        headers: {
          'Authorization': 'Bearer ' + localStorage.getItem("token"),
          'Content-Type': 'application/json'
        }
      })
        .then(response => {
          if (response.ok) {
            // User deleted successfully
            alert("User deleted");
            // deleting the token and user from local storage
            localStorage.removeItem("token");
            localStorage.removeItem("user");
            localStorage.removeItem("email");
            window.location.href = "/Login";
          } else {
            // Failed to delete user
            alert("Failed to delete user");
          }
        })
        .catch(error => {
          console.error(error);
          alert("An error occurred while deleting user");
        });
    }
  };
  
  
  
  
  
  

  const handleInputChange = (event, field) => {
    const updatedUserData = {...userData};
    updatedUserData[field] = event.target.value;
    setUserData(updatedUserData);
  };

  const saveUserData = () => {
    fetch(`https://localhost:7221/api/Userdatas/${userData.email}`, {
      method: 'PUT',
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem("token"),
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(userData)
    })
      .then(response => {
        if (response.ok) {
          alert("User data saved");
          // Perform any additional actions if needed
        } else {
          alert("Failed to save user data");
        }
      })
      .catch(error => {
        console.error(error);
        alert("An error occurred while saving user data");
      });
  };


  return (
    <div className="gradient-background">
      <Grid container spacing={2}>
        {/* Left Container */}
        <Grid item xs={12} md={6}>
          <Paper style={{ padding: 20 }}>
            <Box display="flex" flexDirection="column" alignItems="center" marginBottom={4}>
              <h1 align="center" style={{ backgroundColor: "lightblue" }}>
                UserData (to be deleted)
              </h1>
  
              <div style={{ display: "flex", flexDirection: "column" }}>
               <TextField
                  label="First Name"
                  value={userData.firstName || ""}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                  onChange={(e) => handleInputChange(e, 'firstName')}
                />
                  <TextField
                  label="Last Name"
                  value={userData.lastName || ""}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                  onChange={(e) => handleInputChange(e, 'lastName')}
                />


                <TextField
                  label="Email"
                  value={userData.email || ""}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                />
                {/* <TextField
                  label="Age"
                  value={userData.age || ""}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                  onChange={(e) => handleInputChange(e, 'age')}
                /> */}
                <TextField
                  label="Weight"
                  value={userData.weight || ""}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                  onChange={(e) => handleInputChange(e, 'weight')}
                />
                <TextField
                  label="Height"
                  value={userData.height || ""}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                  onChange={(e) => handleInputChange(e, 'height')}
                />
                {/* Render other user data fields */}
              </div>
  
              <Button variant="contained" color="secondary" onClick={deleteUser}>
                Delete User
              </Button>
              <Button variant="contained" color="primary" onClick={saveUserData}>
                Save/Update
              </Button>
            </Box>
          </Paper>
        </Grid>
  
        {/* Right Container */}
        <Grid item xs={12} md={6}>
          <Paper
            style={{
              padding: 50,
              maxHeight: "100vh",
              width: "80vh",
              overflow: "auto",
            }}
          >
            <div className="right-Container">
              <h1 align="center" style={{ backgroundColor: "lightblue" }}>
                Favorite Training programs
              </h1>
            </div>
          </Paper>
        </Grid>
  
        <Grid item xs={12} md={12}>
          <Paper style={{ padding: 20 }}>
            <Box display="flex" flexDirection="row" alignItems="center" marginBottom={4}>
              <h1 align="center"  style={{ width: "100", backgroundColor: "lightblue" }}>
                Last Activity:
              </h1>
            </Box>
          </Paper>
        </Grid>
      </Grid>
    </div>
  );
  
  
}


export default User