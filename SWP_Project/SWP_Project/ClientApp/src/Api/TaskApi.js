import React, { useState, useEffect } from 'react';
import axios from 'axios';

function TaskApi(link, callback) {
    const [showTaskApi, updateTaskApi] = React.useState([]);
    const [showAss, updateShowAss] = React.useState([]);
    useEffect(() => {
        fetch('https://localhost:7219/api/assignmentstudents/2')
            .then(res => res.json())
            .then(tasks => {
                console.log(tasks)
                updateTaskApi(tasks)

            })
        fetch('https://localhost:7219/api/assignments')
            .then(res => res.json())
            .then(tasks => {
                console.log(tasks)
                updateShowAss(tasks)

            })
    }, [])
    console.log('student')
    console.log(showTaskApi);
    console.log('asss')
    //console.log(showAss);

    for (let i = 0; i < showAss.length; i++) {
        for (let j = 0; j < showTaskApi.length; j++)
            if (showAss[i].id === showTaskApi[j].taskId) {
                console.log(showAss[i])
            }
    }
   
};
export default TaskApi;