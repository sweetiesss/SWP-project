import React, {useState,useEffect } from "react";
import {
  Card,
  CardBody,
  CardTitle,
  ListGroup,
  CardSubtitle,
  ListGroupItem,
  Button,
} from "reactstrap";
import { FaExclamation } from 'react-icons/fa';

import axios from 'axios';

const Feeds = () => {
    const [showTaskApi, updateTaskApi] = useState([]);
    useEffect(() => {
        axios.get('https://localhost:7219/api/Courses').then(res => {
            updateTaskApi(res.data);
           // console.log(res.data);
        })
        //fetch('https://localhost:7219/api/assignments')
        //   .then(res => res.json())
        //    .then(tasks => {
        //       updateTaskApi(tasks)
        //       console.log(tasks)
        //    })
        //console.log(showTaskApi)
        //updateTaskApi(<TaskApi link='https://localhost:7219/api/assignments' />)
       
    }, [])

    function formatDate(date) {
        const options = { day: '2-digit', month: 'short', year: 'numeric' };
        const formattedDate = new Date(date).toLocaleDateString('en-US', options);

        // Split the formatted date into day, month, and year parts
        const [month, day, year] = formattedDate.split(' ');

        // Convert the month abbreviation to uppercase
        const capitalizedMonth = month.toUpperCase();

        // Return the formatted date with uppercase month abbreviation and desired format
        return `${day} ${capitalizedMonth} ${year}`;
    }
    
  return (
    <Card>
      <CardBody>
        <CardTitle tag="h5">Your current task</CardTitle>
        <CardSubtitle className="mb-2 text-muted" tag="h6">
          The most over due time
        </CardSubtitle>
        <ListGroup flush className="mt-4">
                  {
                      showTaskApi.map((task, index) => (
                     
            <ListGroupItem
              key={index}
              className="d-flex align-items-center p-3 border-0"
            >
              <Button
                className="rounded-circle me-3"
                              size="sm"
                color="primary"
              >
                              <i className="icon"><FaExclamation/></i>
                      </Button>
                      {task.name}
              <small className="ms-auto text-muted text-small">
                              {formatDate(task.dateEnd)}
              </small>
            </ListGroupItem>
          ))}
        </ListGroup>
      </CardBody>
    </Card>
  );
};

export default Feeds;
