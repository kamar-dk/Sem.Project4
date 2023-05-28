import React, { useState, useEffect } from 'react';
import {
  Grid, Paper, Typography, Button,
  TextField, Box, Card, CardContent,
} from "@material-ui/core";
import { TextFields } from '@material-ui/icons';


function User() {
  const [userData, setUserData] = useState({});
  const [loading, setLoading] = useState(true);
  const [favoriteTrainingPrograms, setFavoriteTrainingPrograms] = useState([]);



  const getFavoriteTrainingPrograms = async () => {
    const email = localStorage.getItem("email");
    const url = `https://localhost:7221/api/FavoriteTraningPrograms/${email}`;

    try {
      const response = await fetch(url, {
        method: 'GET',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        }
      });

      if (response.ok) {
        const data = await response.json();
        setFavoriteTrainingPrograms(data);
      } else {
        throw new Error('Error fetching favorite training programs:', response.statusText);
      }
    } catch (error) {
      console.error(error);
      alert('Error fetching favorite training programs');
    }
  };

  // //fetching user data using UserDatas controller
  // const fetchUserData = async () => {
  //   const email = localStorage.getItem("email");
  //   const url = `https://localhost:7221/api/UserDatas/${email}`;

  //   try {
  //     const response = await fetch(url, {
  //       method: 'GET',
  //       headers: {
  //         'Accept': 'application/json',
  //         'Content-Type': 'application/json',
  //       },
  //     });

  //     if (response.ok) {
  //       const data = await response.json();
  //       setUserData(data);
  //     } else {
  //       console.error('Error fetching user data:', response.statusText);
  //     }
  //   } catch (error) {
  //     console.error('Error fetching user data:', error);
  //   }

  //   setLoading(false);
  // };
  const fetchUserData = async () => {
    const email = localStorage.getItem("email");
    const url = `https://localhost:7221/api/UserDatas/${email}`;
  
    try {
      const response = await fetch(url, {
        method: 'GET',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
        },
      });
  
      if (response.ok) {
        const data = await response.json();
        const userResponse = await fetch(`https://localhost:7221/api/Users/${email}`);
        if (userResponse.ok) {
          const userData = await userResponse.json();
          data.user = userData;
          setUserData(data);
        } else {
          throw new Error('Error fetching user:', userResponse.statusText);
        }
      } else {
        console.error('Error fetching user data:', response.statusText);
      }
    } catch (error) {
      console.error('Error fetching user data:', error);
    }
  
    setLoading(false);
  };

  // const fetchUserName = async () => {
  //   const email = localStorage.getItem("email");
  //   const url = `https://localhost:7221/api/Users/${email}`;
  
  //   try {
  //     const response = await fetch(url, {
  //       method: 'GET',
  //       headers: {
  //         'Accept': 'application/json',
  //         'Content-Type': 'application/json'
  //       }
  //     });
  
  //     if (response.ok) {
  //       const data = await response.json();
  //       setUserData(data);
  //     } else {
  //       throw new Error('Error fetching user name:', response.statusText);
  //     }
  //   } catch (error) {
  //     console.error(error);
  //     alert('Error fetching user name');
  //   }
  // };

  useEffect(() => {
    fetchUserData();
    getFavoriteTrainingPrograms();


  }, []);

  const deleteUser = () => {
    const confirmed = window.confirm("Are you sure you want to delete your user account?");

    if (confirmed) {

      const deleteUserUrl = `https://localhost:7221/api/Users/${userData.email}`;

      fetch(deleteUserUrl, {
        method: 'DELETE',
        headers: {
          "Accept": "application/json",
          "Content-Type": "application/json",
        }
      })
        .then(response => {
          if (response.ok) {
            alert("User deleted");
            localStorage.removeItem("user");
            localStorage.removeItem("email");
            window.location.href = "/Login";
          } else {
            alert("Failed to delete user");
          }
        })
        .catch(error => {
          console.error(error);
          alert("An error occurred while deleting user");
        });
    }
  };

  const deleteFavoriteTrainingProgram = (id) => {
    const confirmed = window.confirm("Are you sure you want to remove this favorite training program?");
    if (confirmed) {
      fetch(`https://localhost:7221/api/FavoriteTraningPrograms/${userData.email}/${id}`, {
        method: 'DELETE',
        headers: {
          "Accept": "application/json",
          "Content-Type": "application/json",
        }
      })
        .then(response => {
          if (response.ok) {
            // Remove the removed program from the state
            setFavoriteTrainingPrograms(prevState => prevState.filter(program => program.favoriteTraningProgramsID !== id));
            alert("Favorite training program removed");
          } else {
            alert("Failed to remove favorite training program");
          }
        })
        .catch(error => {
          console.error(error);
          alert("An error occurred while removing the favorite training program");
        });
    }
  };

  const handleInputChange = (event, field) => {
    const updatedUserData = { ...userData };
  
    if (field === 'firstName' || field === 'lastName') {
      updatedUserData.user[field] = event.target.value;
    } else {
      updatedUserData[field] = event.target.value;
    }
  
    setUserData(updatedUserData);
  };
  

  //updating userData but no firstname and lastname
  const saveUserData = () => {
   
    const updatedUserData = { ...userData };
    fetch(`https://localhost:7221/api/UserDatas/${userData.email}`, {
      method: 'PUT',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(updatedUserData),
    })
      .then(response => {
        if (response.ok) {
          alert("User data saved");
        } else {
          alert("Failed to save user data");
        }
      })
      .catch(error => {
        console.error(error);
        alert("An error occurred while saving user data");
      });
  };
  

  if (loading) {
    return <p>Loading user data...</p>;
  }

  if (!userData) {
    return <p>Error: User data not found.</p>;
  }

  return (
    <div className="gradient-background">
      <Grid container spacing={2}>
        
        {/* Left Container */}
        <Grid item xs={12} md={6}>
          <Paper style={{ padding: 20 }}>
            <Box display="flex" flexDirection="column" alignItems="center" marginBottom={4}>
              <h1 align="center" style={{ backgroundColor: "lightblue" }}>
                {userData?.user?.firstName} {userData?.user?.lastName}
              </h1>
              <TextField
                label="firstName"
                value={userData?.user?.firstName || ""}
                variant="outlined"
                margin="normal"
                onChange={(e) => handleInputChange(e, 'firstName')}
              />
               <TextField
                label="lastName"
                value={userData?.user?.lastName || ""}
                variant="outlined"
              
                margin="normal"
                onChange={(e) => handleInputChange(e, 'lastName')}
              />

              <div style={{ display: "flex", flexDirection: "column" }}>
                <TextField
                  label="Email"
                  value={userData.email || ""}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                  disabled
                />
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
                <TextField
                  label="DOB"
                  value={userData.doB || ""}

                  fullWidth
                  margin="normal"
                  onChange={(e) => handleInputChange(e, 'doB')}
                />
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


        {/* Favorite Training Programs */}
        <Grid item xs={12} md={6}>
          <Paper style={{ padding: 50, maxHeight: "100vh", width: "80vh", overflow: "auto" }}>
            <div className="right-Container">
              <h1 align="center" style={{ backgroundColor: "lightblue" }}>
                Favorite Training Programs
              </h1>
              {favoriteTrainingPrograms.map(program => (
                <Card key={program.favoriteTraningProgramsID} style={{ backgroundImage: `url(${program.imageUrl})`, backgroundSize: 'cover' }}>
                  <CardContent style={{ textAlign: 'center' }}>
                    <Typography variant="h5" component="h2">
                      {program.name}
                    </Typography>
                    {/* Add more content or details about the training program */}
                  </CardContent>
                  <Button variant="contained" color="secondary"
                    onClick={() => deleteFavoriteTrainingProgram(program.favoriteTraningProgramsID)}
                  >
                    Remove
                  </Button>
                </Card>
              ))}
            </div>
          </Paper>
        </Grid>

    
      </Grid>
    </div>
  );
}

export default User;
