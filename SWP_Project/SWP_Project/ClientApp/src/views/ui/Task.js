import React, { useState, useEffect,useContext } from "react";
import { Table, formGroup } from 'reactstrap'; 
//import { AccountContext } from "../App";
import { AccountContext } from "../../beginlayout/BeginPage";

const Task = () => {
    var num = 1;
    const [showTaskApi, updateTaskApi] = React.useState([]);
    const [task, updateTask] = React.useState([]);
    const courseCon = useContext(AccountContext);
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
                taskArray.push(showTaskApi[i])
            }
        }
        updateTask(taskArray)
    },[showTaskApi]);


    const [taskId, updateTaskId] = useState('');
    const [taskName, updateTaskName] = useState('');
    const [taskDescription, updateTaskDescription] = useState('');
    const [taskDateStart, updateTaskDateStart] = useState(Date);
    const [taskDateEnd, updateTaskDateEnd] = useState(Date);
    

  return (
      <div>
          <form>
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
                      <tr>
                          <th scope="row">
                              {num++}
                          </th>
                          <td>
                              <input type="text" style={{ width: 90, height: 30 }} />
                          </td>
                          <td>
                              <input type="text" style={{ width: 90, height: 30 }} />
                          </td>
                          <td>
                              <input type="text" style={{ width: 80, height: 30 }} />
                          </td>                          <td>
                              <input type="date" style={{ width: 170, height: 30 }} />
                          </td>                          <td>
                              <input type="date" style={{ width: 170, height: 30 }} />
                          </td>
                          <td>
                              <input type="input" disabled style={{ width: 65, height: 30 }} />
                          </td>
                          <td>
                              <button
                                  style={{ width: 108 }}>
                                  Create Task
                              </button></td>
                      </tr>
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
                              {x.status }
                          </td>
                          <td>
                              <button>Delete</button>
                              <button>Edit</button>
                          </td>
                  </tr>
                  ))}

              </tbody>
          </Table>
          </form>
    </div>
  );
};

export default Task;
