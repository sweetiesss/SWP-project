import React, { useState, useEffect, useContext } from "react";
import { Table, formGroup } from 'reactstrap';


const UpdatePage = (props, { resetEditId }) => {
    const object = props.object.task
    const link = props.link
    console.log(object)
    console.log(link)
    const [id, updateId] = React.useState(object.id);
    const [name, updateName] = React.useState(object.name);
    const [description, updateDescription] = React.useState(object.description);
    const [dateStart, updateDateStart] = React.useState(object.dateStart);
    const [dateEnd, updateDateEnd] = React.useState(object.dateEnd);
    const [selectedTask, upDateSelectedTask] = React.useState(object);

    async function updateData(data = {},path) {
        try {
            
            console.log(path);
            const response = await fetch(path, {
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
        console.log(selectedTask.name)
        selectedTask.name = result.name;
        selectedTask.description = result.description;
        selectedTask.dateStart = result.dateStart;
        selectedTask.dateEnd = result.dateEnd;
        e.preventDefault();
        updateData(selectedTask, link);
        resetEditId
        };
    }

    return (
        <form onSubmit={handleUpdate}>
            <input type='text' value={id} name='id' readOnly></input><br />
            <input type='text' value={name} name='name' onChange={(e) => updateName(e.target.value)}></input><br />
            <input type='text' value={description} name='description' onChange={(e) => updateDescription(e.target.value)}></input><br />
            <input type='datetime-local' value={dateStart} name='dateStart' onChange={(e) => updateDateStart(e.target.value)}></input><br />
            <input type='datetime-local' value={dateEnd} name='dateEnd' onChange={(e) => updateDateEnd(e.target.value)}></input><br />
            <button>Update</button>
        </form>
        )
}
export default UpdatePage;