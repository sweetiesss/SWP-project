import React, { useState, useEffect, useContext } from "react";
import { Table, FormGroup, Form, Label, Input } from 'reactstrap';


const UpdatePage = (props) => {
    const object = props.object
    const link = props.link
    const object2= props.object2
    const link2 = props.link2
    const [id, updateId] = React.useState(object2.id);
    const [name, updateName] = React.useState(object2.name);
    const [description, updateDescription] = React.useState(object2.description);
    const [dateStart, updateDateStart] = React.useState(object2.dateStart);
    const [dateEnd, updateDateEnd] = React.useState(object2.dateEnd);
    const [status, updateStatus] = React.useState(object.status);
    const [selectedTask, upDateSelectedTask] = React.useState(object);
    const [selectedTask2, upDateSelectedTask2] = React.useState(object2);


    async function updateData(data = {}, path) {
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
        console.log(selectedTask)
        selectedTask2.name = result.name;
        selectedTask2.description = result.description;
        selectedTask2.dateStart = result.dateStart;
        selectedTask2.dateEnd = result.dateEnd;
        selectedTask.status = result.status;
    
        e.preventDefault();
        updateData(selectedTask, link);
        updateData(selectedTask2, link2);
        props.resetEditId();

    }

    return (
        <Form onSubmit={handleUpdate} class=''>
            <FormGroup >
                <Label>Id</Label>
                <Input style={{width:400}} type='text' value={id} name='id' readOnly></Input>
            </FormGroup>
            <FormGroup>
                <Label >Name</Label>
                <Input style={{width:400}} type='text' value={name} name='name' onChange={(e) => updateName(e.target.value)}></Input>
            </FormGroup>
            <FormGroup>
                <Label >Description</Label>
                <Input style={{width:400}} type='text' value={description} name='description' onChange={(e) => updateDescription(e.target.value)}></Input>
            </FormGroup>
            <FormGroup>
                <Label >Date Start</Label>
                <Input style={{width:400}} type='datetime-local' value={dateStart} name='dateStart' onChange={(e) => updateDateStart(e.target.value)}></Input>
            </FormGroup>
            <FormGroup>
                <Label >Date End</Label>
                <Input style={{width:400}} type='datetime-local' value={dateEnd} name='dateEnd' onChange={(e) => updateDateEnd(e.target.value)}></Input>
            </FormGroup>
            <FormGroup>
                <Label >Status</Label>
                <Input style={{ width: 400 }} type='select' value={status} name='status' onChange={(e) => updateStatus(e.target.value)} >
                    <option value='Ongoing'>Ongoing</option>
                    <option value='Closed'>Closed</option>
                    <option value='Completed'>Completed</option>
                </Input>
            </FormGroup>
            <FormGroup>
                <button style={{ width: 200 }}>Update</button>
                <button style={{ width: 200 }} onClick={() => { props.resetEditId() } }>Back</button>
                </FormGroup>
            
        </Form>
    )
}
export default UpdatePage;