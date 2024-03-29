import React, { useState, useEffect, useRef } from 'react';
import axios from 'axios';
import { useAuthDataContext } from '../AuthContext';
import TaskRow from '../components/TaskRow';
import { HubConnectionBuilder } from '@microsoft/signalr';

const Home = () => {
    const [tasks, setTasks] = useState([]);
    const [title, setTitle] = useState('');
    const { user } = useAuthDataContext();
    const connectionRef = useRef(null);

    useEffect(() => {
        const connectToHub = async () => {
            const connection = new HubConnectionBuilder().withUrl("/taskupdate").build();
            await connection.start();
            connectionRef.current = connection;

            connection.on('taskUpdated', obj => {
                setTasks(obj);
            });
        }

        const getTasks = async () => {
            const { data } = await axios.get('api/taskitem/getallincomplete');
            setTasks(data);
        }

        connectToHub();
        getTasks();

    }, []);

    const realTimeUpdate = () => {
        const connection = connectionRef.current;
        connection.invoke('taskUpdated');
    }

    const onAddClick = async () => {
        await axios.post(`api/taskitem/add?title=${title}`);
        setTitle('');
        realTimeUpdate();
    }

    const onDoingClick = async (id) => {
        await axios.post('api/taskitem/updatestatus', { taskId: id, status: 1, userId: user.id });
        realTimeUpdate();
    }

    const onDoneClick = async (id) => {
        await axios.post('api/taskitem/updatestatus', { taskId: id, status: 2, userId: user.id });
        realTimeUpdate();
    }

    return (
        <div>
            <div className="row">
                <input type="text" placeholder="Task Title"
                    className="form-control-lg"
                    onChange={e => setTitle(e.target.value)}
                    value={title} />
                <button onClick={onAddClick}
                    className="btn btn-primary">Add Task
                </button>
            </div>
            <br />
            <table className="table table-hover table-striped table-bordered">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    {tasks.map(t => <TaskRow key={t.id}
                        task={t}
                        onDoingClick={() => onDoingClick(t.id)}
                        onDoneClick={() => onDoneClick(t.id)}
                    />)}
                </tbody>
            </table>
        </div>
    )
}

export default Home;