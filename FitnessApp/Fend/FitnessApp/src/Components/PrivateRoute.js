import { Outlet, Navigate } from "react-router-dom";

const PrivateRoutes = ({ children: Component, ...rest }) => {
    let auth = localStorage.getItem("role");
    console.log(auth)
    return (
        auth == "Manager" ? <Outlet /> : <Navigate to="/Main" />
            
    )
}

export default PrivateRoutes;