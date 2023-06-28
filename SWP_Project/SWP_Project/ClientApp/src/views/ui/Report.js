import React, { useState, useEffect } from "react";
import { Form, FormGroup, Label, Input, FormText ,Button} from 'reactstrap'; 

export default function Report() {
    return (
        <div>
        <h2>Report</h2>
        <Form>
            <FormGroup>
                <Label for="exampleEmail">
                    Email
                </Label>
                <Input
                    id="exampleEmail"
                    name="email"
                    placeholder="Email"
                    type="email"
                />
           
            </FormGroup>
            <FormGroup>
                <Label for="exampleFile">
                    File
                </Label>
                <Input
                    id="exampleFile"
                    name="file"
                    type="file"
                />
                <FormText>
                   Clip your file to send.
                </FormText>
            </FormGroup>
            <FormGroup>
                <Label for="exampleText">
                    Text Area
                </Label>
                <Input
                    id="exampleText"
                    name="text"
                    type="textarea"
                />
            </FormGroup>
            <FormGroup check>
                <Input type="checkbox" />
                {' '}
                <Label check>
                    Are you sure to send it.
                </Label>
            </FormGroup>
            <Button>
                Submit
            </Button>
            </Form>
        </div>
    )
}