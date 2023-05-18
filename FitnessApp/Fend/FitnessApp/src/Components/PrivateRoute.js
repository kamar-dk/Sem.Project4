import { Outlet, Navigate } from "react-router-dom";

const PrivateRoutes = ({ children: Component, ...rest }) => {
    let auth = localStorage.getItem("user");
    console.log(auth)
    return (
        auth == "user" ? <Outlet /> : <Navigate to="/Main" />
            
    )
}

export default PrivateRoutes;