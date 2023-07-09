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
    const [link2, updateLink2] = React.useState();
    const [selectedTask, updateSelectedTask] = React.useState();
    const [selectedTask2, updateSelectedTask2] = React.useState();
    const courseCon = useContext(AccountContext);
    console.log(courseCon.accountId)
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
                console.log(showTaskApi[i])
                taskArray.push(showTaskApi[i])
            }
        }
        updateTask(taskArray)
    }, [showTaskApi]);


    const handleSubmit = (e) => {
        const data = new FormData(e.target)
        e.preventDefault()
        const result = Object.fromEntries(data.entries());
        const assStu = {
            "taskId": result.id,
            "studentId": courseCon.accountId,
            "status": "Ongoing",
        }
        postData('https://localhost:7219/api/assignments', result)
        postData('https://localhost:7219/api/assignmentstudents', assStu)
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


    async function deleteData(url = "", data = "") {
        try {
            const link = url + data;
            const result = await fetch(link, {
                method: 'DELETE',
                headers: {
                    "Content-Type": "application/json",
                },
            }).then((response) => {
                if (!response.ok) {
                    throw new Error('Something went Wrong')
                }
            })
        } catch (error) {
            console.error("Error:", error);
        }
    }

    function handleEdit(object, object2, url, url2) {
        const path = url + object.id;
        const path2 = url2 + object2.id;
        updateLink(path);
        updateLink2(path2);
        updateSelectedTask(object);
        updateSelectedTask2(object2);
        setEditId(object.id);
    }

    function resetEditId() {
        setEditId(-1)
    }

    function handleDelete(url,data) {
        deleteData('https://localhost:7219/api/assignments/', data);
    }

    return (
        editId !== -1 ? <UpdatePage object={selectedTask} object2={selectedTask2} link={link} link2={link2} resetEditId={resetEditId} /> :(
            <div>
                <div>
                    <form onSubmit={handleSubmit}>
                        <input type="text" style={{ width: 90, height: 30 }} name='id' />
                        <input type="text" style={{ width: 90, height: 30 }} name='name' />
                        <input type="text" style={{ width: 80, height: 30 }} name='description' />
                        <input type="datetime-local" style={{ width: 170, height: 30 }} name='dateStart' />
                        <input type="datetime-local" style={{ width: 170, height: 30 }} name='dateEnd' />
                        <input type="input" disabled style={{ width: 65, height: 30 }} />
                        <button
                            style={{ width: 108 }} >
                            Create Task
                        </button>
                    </form>
                </div>
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
                                        <button onClick={() => handleDelete('https://localhost:7219/api/assignments/', x.task.id)}>Delete</button>
                                        <button onClick={() => { handleEdit(x,x.task, 'https://localhost:7219/api/assignmentstudents/', 'https://localhost:7219/api/assignments/') }}>Edit</button>
                                    </td>
                                </tr>
                            ))}

                        </tbody>
                    </Table>
                </div>
            </div>
            )
    );
};

export default Task;
