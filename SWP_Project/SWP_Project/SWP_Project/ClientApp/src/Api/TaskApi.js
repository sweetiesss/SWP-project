import React, { useState, useEffect, useContext } from "react";
import { Table, formGroup } from 'reactstrap';
//import { AccountContext } from "../App";


const TaskApi = () => {
    var num = 1;
    const [showTaskApi, updateTaskApi] = React.useState([]);
    const [task, updateTask] = React.useState([]);
    const taskArray = [];
    useEffect(() => {
        (async () => {
            const data = await fetch('https://localhost:7219/api/assignments/')
                .then(res => res.json())
                .then(tasks => {
                    updateTaskApi(tasks)
                })
        })()
    }, []);


    async function postData(url = "", data = {}) {
        try {
            const response = await fetch(url, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            });
            const result = await response.json();
            console.log("Success:", result);
        } catch (error) {
            console.error("Error:", error);
        }
    }
    console.log(showTaskApi)

    async function deleteData(url = "", data = "") {
        try {
            const link = url + data;
            const result=await fetch(link, {
                method: 'DELETE',          
                headers: {
                    "Content-Type": "application/json",
                },
            }).then((response) => {
                if(!response.ok){
                    throw new Error('Something went Wrong')
                }
            })
        } catch (error) {
            console.error("Error:", error);
        }
    }
    return (
        <div>
          
                <Table bordered>
                    <thead>
                        <tr>
                            <th>
                                #
                            </th>
                            <th>
                                Task Id
                            </th>
                            <th>
                                Task Name
                            </th>
                            <th>
                                Description
                            </th>
                            <th>
                                Date Start
                            </th>
                            <th>
                                Date End
                            </th>
                            <th>
                                Status
                            </th>
                            <th>Edit</th>
                        </tr>
                    </thead>
                    <tbody>
                        {showTaskApi.map((x) => (
                            <tr key={x.id}>
                                <th scope="row">
                                    {num++}
                                </th>
                                <td>
                                    {x.id}
                                </td>
                                <td>
                                    {x.name}
                                </td>
                                <td>
                                    {x.descriptione}
                                </td>
                                <td>
                                    {x.dateStart}
                                </td>
                                <td>
                                    {x.dateEnd}
                                </td>
                                <td>
                                    <button onClick={()=>deleteData('https://localhost:7219/api/assignments/', x.id)}>Delete</button>
                                    <button>Edit</button>
                                </td>
                            </tr>
                        ))}

                    </tbody>
                </Table>

        </div>
    );
};

export default TaskApi;




 
