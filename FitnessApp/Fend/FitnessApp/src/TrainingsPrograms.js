import React, { useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Card, CardMedia, CardActions, IconButton } from '@material-ui/core';
import { Album } from '@material-ui/icons';

const useStyles = makeStyles((theme) => ({
  root: {
    maxWidth: 345,
    margin: theme.spacing(2),
  },
  media: {
    height: 0,
    paddingTop: '56.25%', // 16:9
  },
  actions: {
    display: 'flex',
    justifyContent: 'flex-end',
  },
}));



export default function TrainingPrograms() {
  const classes = useStyles();
  const [favorites, setFavorites] = useState([]);

  const handleFavorite = (program) => {
    if (favorites.find((item) => item.id === program.id)) {
      setFavorites((prevState) => prevState.filter((item) => item.id !== program.id));
    } else {
      setFavorites((prevState) => [...prevState, program]);
    }
  };
  
  const programs = [
    {
      id: 1,
      title: 'Fitness Training',
      image: 'https://source.unsplash.com/weekly?fitness',
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.',
    },
    {
      id: 2,
      title: 'Yoga Practice',
      image: 'https://source.unsplash.com/weekly?yoga',
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.',
    },
    {
      id: 3,
      title: 'Meditation',
      image: 'https://source.unsplash.com/weekly?meditation',
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.',
    },
  ];

  return (
    <>
      <h1>Training Programs</h1>
      <div>
        {programs.map((program) => (
          <Card className={classes.root} key={program.id}>
            <CardMedia className={classes.media} image={program.image} title={program.title} />
            <CardActions className={classes.actions}>
              <IconButton onClick={() => handleFavorite(program)}>
                <Album color={favorites.find((item) => item.id === program.id) ? 'secondary' : 'disabled'} />
              </IconButton>
            </CardActions>
          </Card>
        ))}
      </div>
    </>
  );
};

