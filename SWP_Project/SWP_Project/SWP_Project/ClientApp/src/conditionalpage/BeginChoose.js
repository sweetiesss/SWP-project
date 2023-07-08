import React, { useState, useEffect,useContext } from 'react';

import axios from 'axios';
import { ListGroup, ListGroupItem, Badge, UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem } from "reactstrap";
//import { AccountContext } from "../App";
import { AccountContext } from "../beginlayout/BeginPage";

export default function BeginChoose(props) {
    const roleAcc = useContext(AccountContext);
    const [semester, updateSemesters] = useState([]);
    useEffect(() => {
        axios.get('https://localhost:7219/api/semesters').then(res => {
            updateSemesters(res.data);
        })
    }, [])

    return (
        
        <div>
            {/*roleAcc.role.accountId*/}
            {!semester ? (<UncontrolledDropdown group>
                <DropdownToggle
                    data-toggle="dropdown"
                    color="primary"
                    tag="span"
                >
                    <ListGroup>
                        <ListGroupItem className="justify-content-between">
                        <div>
                                error{'   '}
                            <Badge pill>
                                1
                                </Badge>
                            </div>
                        </ListGroupItem>
                    </ListGroup>
                </DropdownToggle>
                <DropdownMenu>
                    <DropdownItem header>
                        Course
                    </DropdownItem>
                    <DropdownItem onClick={() => props.handeClickCourse('Test')}>
                        <strong>TEST Course</strong> This is the test course accordion body.
                        This course only show when our Datat&#39;s Api is not working.<br/> <code>.Api:false</code>, though the function will run normally.
                    </DropdownItem>
                    <DropdownItem onClick={() => props.handeClickCourse('Test2')}>
                    <div>
                        <strong>TEST2 Course</strong> This is the second test course accordion body for who dont like the first test course.
                            This course only show when our Datat&#39;s Api is not working. <br/><code>.Api:false</code>, though the function will run normally.</div>
                    </DropdownItem>
                </DropdownMenu>
            </UncontrolledDropdown>)
                :
                semester.map((sem) => {
                    return (
                        <div>
            <UncontrolledDropdown group>
                <DropdownToggle
                    data-toggle="dropdown"
                    color="primary"
                    tag="span">
                    <ListGroup>
                                    <ListGroupItem className="justify-content-between" key={sem.id}>
                                {sem.name}{' '}
                            <Badge pill>
                                            {sem.courses.length}
                                        </Badge>
                        </ListGroupItem>
                    </ListGroup>
                </DropdownToggle>
                    <DropdownMenu>
                        <DropdownItem header>
                            Course
                        </DropdownItem>
                        {sem.courses.map((course) => { 
                            return(
                                <DropdownItem onClick={() => props.handeClickCourse(course.id)}>
                                    {course.subjectId} - {course.name} - {course.lecturerId}
                    </DropdownItem>
                            )})}
                </DropdownMenu>
                            </UncontrolledDropdown>
                            <br/>
                        </div>
                 
            )})}
        </div>
    )
}