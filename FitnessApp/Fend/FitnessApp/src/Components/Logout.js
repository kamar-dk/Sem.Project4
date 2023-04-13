import React from 'react';

function Footer(){
    return(
        <div>{Logout()}
        </div>
        
    )
}

function Logout(){
    localStorage.removeItem("token");
    localStorage.removeItem("ModelId");
    localStorage.removeItem("role");
    window.location.href = "/Login";
}

export default Footer;