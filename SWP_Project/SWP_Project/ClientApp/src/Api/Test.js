import React, { useState, useEffect, useContext } from "react";
import { Table, formGroup } from 'reactstrap';
//import { AccountContext } from "../App";


const Task = () => {
    var num = 1;
    const [showTaskApi, updateTaskApi] = React.useState([]);
    const [task, updateTask] = React.useState([]);
    const [accountId, setAccountId] = useState('SE173508')
    const [selectedId, setSelectedId] = useState(-1);
    const [selectedTask, setSelectedTask] = useState();
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
            if (showTaskApi[i].studentId === accountId) {
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

    function editButton(x, task) {
        setSelectedId(x)
        setSelectedTask(task)
    }
    function EditInput(select) {
        return (
            <tr>
                <th scope="row">
                    {num++}
                </th>
                <td>
                    <input type="text" style={{ width: 90, height: 30 }} name='id' value={selectedTask.id} />
                </td>
                <td>
                    <input type="text" style={{ width: 90, height: 30 }} name='name' value={selectedTask.name } />
                </td>
                <td>
                    <input type="text" style={{ width: 80, height: 30 }} name='description' value={selectedTask.description} />
                </td>
                <td>
                    <input type="datetime-local" style={{ width: 170, height: 30 }} name='dateStart' value={selectedTask.dateStart} />
                </td>
                <td>
                    <input type="datetime-local" style={{ width: 170, height: 30 }} name='dateEnd' value={selectedTask.dateEnd} />
                </td>
                <td>
                    <input type="input" disabled style={{ width: 65, height: 30 }} />
                </td>
                <td>
                    <button>
                        update
                    </button>
                    <button onClick={() => setSelectedId(-1)}>
                        Cancel
                    </button>
                </td>
            </tr>
        )
    }
    return (
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

                            x.id === selectedId ? <EditInput /> :
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
                                        <button onClick={() => editButton(x.id, x.task)}>Edit</button>
                                    </td>
                                </tr>
                        ))}

                    </tbody>
                </Table>
            </div>
        </div>
    );
};

export default Task;
