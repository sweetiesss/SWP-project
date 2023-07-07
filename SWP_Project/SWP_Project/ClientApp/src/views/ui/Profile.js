import React, { useState, useEffect, useContext } from "react";
import { Form, FormGroup, Label, Input, FormText, Button,Col } from 'reactstrap';
import { AccountContext } from "../../beginlayout/BeginPage";
export default function Profile() {
    const courseCon = useContext(AccountContext);
    const [showTaskApi, updateTaskApi] = React.useState([]);
    useEffect(() => {
        (async () => {
            const data = await fetch('https://localhost:7219/api/students/' + courseCon.accountId)
                .then(res => res.json())
                .then(tasks => {
                    updateTaskApi(tasks)
                })
        })()
    }, []);
    console.log(showTaskApi)
    return (
        <div>
            <h2>Profile</h2>
            <Form>
                <FormGroup>
                    <Label for="name">
                        Name
                    </Label>
                    <Input
                        id="name"
                        name="name"
                        placeholder={showTaskApi.name}
                        type="text"
                    />
                </FormGroup>
                {/*Course*/ }

                {/*Course*/ }
                    <Button>
                        Edit
                    </Button>
            </Form>
        </div>
    )
}