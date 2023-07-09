import React, { useState, useEffect, useContext } from "react";
import { Table, formGroup } from 'reactstrap';
import { Link } from "react-router-dom";
import UpdatePage from './UpdatePage';
//import { AccountContext } from "../App";
import { AccountContext } from "../../beginlayout/BeginPage";


const Task = () => {
    var num = 1;
    const [showTaskApi, updateTaskApi] = React.useState([]);
    const [task, updateTask] = React.useState([]);
    const [editId, setEditId] = React.useState(-1);
    const [link, updateLink] = React.useState();
    const [selectedTask, updateSelectedTask] = React.useState();
    const courseCon = useContext(AccountContext);
    const taskArray = [];
    useEffect(() => {
        (async () => {
            const data = await fetch('https://localhost:7219/api/assignmentstudents/')
                .then(res => res.json())
                .then(tasks => {
                    updateTaskApi(tasks)
                })
        })()
    }, []);
    
    useEffect(() => {
        for (let i = 0; i < showTaskApi.length; i++) {
            if (showTaskApi[i].studentId === courseCon.accountId) {
                taskArray.push(showTaskApi[i])
            }
        }
        updateTask(taskArray)
    }, [showTaskApi]);


    const handleSubmit = (e) => {
        const data = new FormData(e.target)
        e.preventDefault()
        console.log(Object.fromEntries(data.entries()))
        postData('https://localhost:7219/api/assignments', Object.fromEntries(data.entries()))
    }

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

    async function deleteData(url = "", data = {}) {
        try {
            const response = await fetch(url + data);
        } catch (error) {
            console.error("Error:", error);
        }
    }

    function handleEdit(object, url, location) {
        console.log(location)
        const path = url + location;
        updateLink(path);
        updateSelectedTask(object);
        setEditId(location);

    }

    function resetEditId() {
        setEditId(-1)
    }
    console.log(task)
    return (
        editId !== -1 ? <UpdatePage object={selectedTask} link={link} setEditId={resetEditId} />:
    <div>
        <form onSubmit={handleSubmit}>
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
                    <tr>
                        <th scope="row">
                            {num++}
                        </th>
                        <td>
                            <input type="text" style={{ width: 90, height: 30 }} name='id' />
                        </td>
                        <td>
                            <input type="text" style={{ width: 90, height: 30 }} name='name' />
                        </td>
                        <td>
                            <input type="text" style={{ width: 80, height: 30 }} name='description' />
                        </td>                          <td>
                            <input type="datetime-local" style={{ width: 170, height: 30 }} name='dateStart' />
                        </td>                          <td>
                            <input type="datetime-local" style={{ width: 170, height: 30 }} name='dateEnd' />
                        </td>
                        <td>
                            <input type="input" disabled style={{ width: 65, height: 30 }} />
                        </td>
                        <td>
                            <button
                                style={{ width: 108 }} >
                                Create Task
                            </button></td>
                    </tr>
                            {task.map((x) => (
                        <tr key={x.id}>
                            <th scope="row">
                                {num++}
                            </th>
                            <td>
                                {x.task.id}
                            </td>
                            <td>
                                {x.task.name}
                            </td>
                            <td>
                                {x.task.description}
                            </td>
                            <td>
                                {x.task.dateStart}
                            </td>
                            <td>
                                {x.task.dateEnd}
                            </td>
                            <td>
                                {x.status}
                            </td>
                            <td>
                                <button>Delete</button>
                            
                                <button onClick={() => {handleEdit(x,'https://localhost:7219/api/assignments/',x.task.id)}}>Edit</button>
                            </td>
                        </tr>
                    ))}

                </tbody>
            </Table>
        </form>
    </div>
  );
};

export default Task;
