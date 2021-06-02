import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useAuthDataContext } from '../AuthContext';

const TaskRow = ({ task, onDoingClick, onDoneClick }) => {

    const { user } = useAuthDataContext();
    const [userDoing, setUserDoing] = useState({firstName: '', lastName: ''});

    useEffect(() => {
        const getUserById = async () => {
            const { data } = await axios.get(`/api/account/getuserbyid?userId=${task.userId}`);
            setUserDoing(data);
        }

        getUserById();

    });

    return (
        <tr>
            <td>{task.title}</td>
            <td>
                {task.status === 0 &&
                    <button onClick={onDoingClick}
                        className="btn btn-outline-success">I got this!
                            </button>}
                {task.status === 1 && task.userId === user.id &&
                    <button onClick={onDoneClick}
                        className="btn btn-outline-primary">I'm done!
                            </button>}
                {task.status === 1 && task.userId !== user.id && 
                    <button className="btn btn-outline-danger"
                        disabled> {userDoing.firstName} {userDoing.lastName} is doing
                            </button>}
            </td>
        </tr >
    )
}

export default TaskRow;