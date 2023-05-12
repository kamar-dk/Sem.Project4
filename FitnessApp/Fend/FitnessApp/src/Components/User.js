import React from 'react';
import { useState } from 'react';
import { Parallax } from 'react-parallax';

function User()
{
    return (
        <div style={{ height: "100vh", position: "relative" }}>
          <Parallax bgImage="/images/1.jpg" strength={5}>
            <div
              style={{
                height: "100vh",
                display: "flex",
                flexDirection: "column",
                justifyContent: "center",
                alignItems: "center",
                color: "white",
                textShadow: "2px 2px 4px #000000",
              }}
            >
                <h1>Insert new user stuff here</h1>
            </div>
          </Parallax>
          </div>
      );
        
}

export default User