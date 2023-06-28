import React, { useState, useEffect } from "react";
import { Table } from 'reactstrap'; 

const Task = () => {
  // For Dismiss Button with Alert
    const [showTaskApi, updateTaskApi] = React.useState([]);
    useEffect(() => {
        fetch('https://localhost:7219/api/assignments')
            .then(res => res.json())
            .then(tasks => {
                updateTaskApi(tasks)
            })
    }, [])
    var num = 1;
  return (
      <div>
          <Table>
              <thead>
                  <tr>
                      <th>
                          #
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
                      <th>Edit</th>
                  </tr>
              </thead>
              <tbody>
                  {showTaskApi.map((task) => (
                      <tr>
                          <th scope="row">
                              {num++}
                          </th>
                          <td>
                              {task.name}
                          </td>
                          <td>
                              {task.description}
                          </td>
                          <td>
                              {task.dateStart}
                          </td>
                          <td>
                              {task.dateEnd}
                          </td>
                          <td>
                              <button>Delete</button>
                              <button>Edit</button>
                          </td>
                  </tr>
                  ))}
              </tbody>
          </Table>
    </div>
  );
};

export default Task;
