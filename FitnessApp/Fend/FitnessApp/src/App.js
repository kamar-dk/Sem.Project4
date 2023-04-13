import React from 'react';
import Main from './Components/Main';
import {Route, Routes, Link } from 'react-router-dom';
import Login from './Login';
import Calories from './Components/Calories';
import Activity from './Components/Activity';
import TilfojManager from './Components/TilfojManager';
import "./Components/NavBar.css"
import Logout from './Components/Logout';
import TilfojModelTilJob from './Components/TilfojModelTilJob';
import Tracking from './Components/Tracking';
import PrivateRoutes from './Components/PrivateRoute';
import TilfojExpense from './Components/TilfojExpense';
import SletModelFraJob from './Components/SletModelFraJob';

function App() {
  const token = localStorage.getItem("token")

  if(!token){
    return <Login/>
  }

  return (
    <>
    <nav>
      <u1>
        <li><Link to="/Main">Main</Link></li>
        <li><Link to="/Calories">Calories</Link></li>
        <li><Link to="/Activity">Activity</Link></li>
        <li><Link to="/Tracking">Tracking</Link></li>
        <li><Link to="/TilfojModel">Tilføj model</Link></li>
        <li><Link to="/TilfojManager">Tilføj manager</Link></li>
        <li><Link to="/TilfojExpense">Udgifter til job</Link></li>
        <li><Link to="/SletModelFraJob">Slet model fra job</Link></li>
        <li><Link to="/Logout">Logout</Link></li>
      </u1>
    </nav>
    <Routes>
      <Route element={<PrivateRoutes/>}>
        <Route path="/Activity" element={<Activity/>} ></Route>
        <Route path="/TilfojManager" element={<TilfojManager/>} ></Route>
        <Route path="/Calories" element={<Calories/>} ></Route>
        <Route path="/ModelTilJob" element={<TilfojModelTilJob/>} ></Route>
        <Route path="/SletModelFraJob" element={<SletModelFraJob/>} ></Route>
      </Route>
      <Route path="/Main" element={<Main/>} ></Route>
      <Route path="/Tracking" element={<Tracking/>} ></Route>
      <Route path="/TilfojExpense" element={<TilfojExpense/>} ></Route>
      <Route path="/Logout" element={<Logout/>} ></Route>
    </Routes>
    </>
  );
}

export default App;
