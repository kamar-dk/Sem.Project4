import React, { useEffect, useState } from "react";
import "./Lists.css";
function Tracking(){
    const [data, setData] = useState([]);
    const [id, setId] = useState([])
    const fetchData = () => {
        var url = "https://localhost:7181/api/Jobs";
        return fetch(url, {
            method: 'GET',
            credentials: 'include',
            headers: {
              'Authorization': 'Bearer ' + localStorage.getItem("token"),
              'Content-Type': 'application/json'
            }
            })
              .then((response) => response.json())
              .then((data) => setData(data));
        }
    

    useEffect(() => {
        fetchData();
    }, [id]);
    return(
        <React.Fragment>
                <h1>Jobs</h1>
                <h3>
                    {data.map}
                  {data.map(item => (
                    <tr key={item.firstName}>
                        <td>Customer:</td>
                      <td>{item.customer}</td>
                        <td>startDate:</td>
                      <td>{item.startDate}</td>
                        <td>days:</td>
                      <td>{item.days}</td>
                        <td>location:</td>
                      <td>{item.location}</td>
                        <td>comments:</td>
                      <td>{item.comments}</td>
                    </tr>
                  ))}
                </h3>
        </React.Fragment>
    )

}
export default Tracking;