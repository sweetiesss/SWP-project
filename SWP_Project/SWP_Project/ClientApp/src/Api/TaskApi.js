import React, { useState, useEffect, useContext } from "react";
import { Table, formGroup } from 'reactstrap';
//import { AccountContext } from "../App";


const TaskApi = () => {
    var num = 1;
    const [showTaskApi, updateTaskApi] = React.useState([]);
    const [id, updateId] = React.useState([]);
    const [name, updateName] = React.useState();
    const [description, updateDescription] = React.useState();
    const [dateStart, updateDateStart] = React.useState();
    const [dateEnd, updateDateEnd] = React.useState();
    const [selectedTask, upDateSelectedTask] = React.useState();
    const taskArray = [];
    function selectAss(object) {
        updateId(object.id)
        updateName(object.name);
        updateDescription(object.description)
        updateDateStart(object.dateStart)
        updateDateEnd(object.dateEnd)
        upDateSelectedTask(object)
        console.log(object.assignmentStudents[0].student)
   
        console.log(object)
    }
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
    async function updateData(url = "", id = " ", data = {}) {
        try {
            const link = url + id;
            console.log(link);
            const response = await fetch(link, {
                method: "PUT",
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
    const handleUpdate = (e) => {
        const data = new FormData(e.target)
        const result = Object.fromEntries(data.entries());
        console.log(result.name)
        selectedTask.name = result.name;
        selectedTask.description = result.description;
        selectedTask.dateStart = result.dateStart;
        selectedTask.dateEnd = result.dateEnd;
        //delete selectedTask.id;
        console.log(selectedTask);
                    e.preventDefault();
        updateData('https://localhost:7219/api/assignments/', selectedTask.id, selectedTask);
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
                                    {x.description}
                                </td>
                                <td>
                                    {x.dateStart}
                                </td>
                                <td>
                                    {x.dateEnd}
                                </td>
                                <td>
                                    <button onClick={()=>deleteData('https://localhost:7219/api/assignments/', x.id)}>Delete</button>
                                    <button onClick={() =>selectAss(x)}>Edit</button>
                                </td>
                            </tr>
                        ))}

                    </tbody>
            </Table>
         
            {showTaskApi.length != 0 ? 
                <div>
                <form onSubmit={handleUpdate}>
                    <input type='text' value={id} name='id' readOnly></input><br/>
                    <input type='text' value={name} name='name' onChange={(e) => updateName(e.target.value)}></input><br />
                    <input type='text' value={description} name='description' onChange={(e) => updateDescription(e.target.value)}></input><br />
                    <input type='datetime-local' value={dateStart} name='dateStart' onChange={(e) => updateDateStart(e.target.value)}></input><br />
                    <input type='datetime-local' value={dateEnd} name='dateEnd' onChange={(e) => updateDateEnd(e.target.value)}></input><br />
                <button>Update</button>
                </form>
                </div> : <h2>sorry</h2>}
         </div>
    );
};

export default TaskApi;




 
