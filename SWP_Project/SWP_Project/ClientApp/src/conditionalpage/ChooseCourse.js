import React, { useState, useEffect } from 'react';

import axios from 'axios';
import {
    ListGroup, ListGroupItem, Badge, UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem, Accordion,
    AccordionBody,
    AccordionHeader,
    AccordionItem,
Button} from "reactstrap";
export default function SemesterOption(props) {

    const [semester, updateSemesters] = useState();
    useEffect(() => {
        axios.get('https://localhost:7219/api/semesters').then(res => {
            updateSemesters(res.data);

        })
    }, [])
    console.log(props)
    const [open, setOpen] = useState('');
    const toggle = (id) => {
        if (open === id) {
            setOpen();
        } else {
            setOpen(id);
        }
    };
    return (
        <div>
            {!semester ? (
            <div>
                <Accordion open={open} toggle={toggle}>
                    <AccordionItem>
                            <AccordionHeader targetId="1">  <div> error{' '}<Badge>1</Badge> {'  '}this course will disapear when api is working again or the web is lunching.</div></AccordionHeader>
                            <AccordionBody accordionId="1" >
                                <Button outline onClick={() => props.handeClickCourse('test')}>Test</Button>
                                {"     "}<strong>This is the test course accordion body.</strong>
                          This course only show when our Datat&#39;s Api is not working. <code>.Api:false</code>, though the function will run normally.
                            </AccordionBody>
                            <AccordionBody>
                            hello</AccordionBody>
                    </AccordionItem>
                    <AccordionItem>
                        <AccordionHeader targetId="2">Accordion Item 2</AccordionHeader>
                        <AccordionBody accordionId="2">
                            <strong>This is the second item&#39;s accordion body.</strong>
                            You can modify any of this with custom CSS or overriding our default
                            variables. It&#39;s also worth noting that just about any HTML can
                            go within the <code>.accordion-body</code>, though the transition
                            does limit overflow.
                        </AccordionBody>
                    </AccordionItem>
                    <AccordionItem>
                        <AccordionHeader targetId="3">Accordion Item 3</AccordionHeader>
                        <AccordionBody accordionId="3">
                            <strong>This is the third item&#39;s accordion body.</strong>
                            You can modify any of this with custom CSS or overriding our default
                            variables. It&#39;s also worth noting that just about any HTML can
                            go within the <code>.accordion-body</code>, though the transition
                            does limit overflow.
                        </AccordionBody>
                    </AccordionItem>
                </Accordion>
            </div>
  ) : semester.map((sem) => {
                return (
                    <UncontrolledDropdown group>
                        <DropdownToggle
                            data-toggle="dropdown"
                            color="primary"
                            tag="span">
                            <ListGroup>
                                <ListGroupItem className="justify-content-between">
                                    {sem.Name}{' '}
                                    <Badge pill>
                                        {sem.Courses.length}
                                    </Badge>
                                </ListGroupItem>
                            </ListGroup>
                        </DropdownToggle>
                        <DropdownMenu>
                            <DropdownItem header>
                                Course
                            </DropdownItem>
                            {sem.Courses.map((course) => {
                                return (
                                    <DropdownItem>
                                        {course.SubjectId} - {course.Name} - {course.LecturerId}
                                    </DropdownItem>
                                )
                            })}
                        </DropdownMenu>
                    </UncontrolledDropdown>
                )
            })}
        </div>
    )
}