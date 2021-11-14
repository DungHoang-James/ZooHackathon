import { useEffect, useState } from 'react';
import api from './api';
import './App.css';

function App() {
	const [data, setData] = useState([]);

	useEffect(() => {
		async function getData() {
			const res = await api.get(`/api/users`);
			if (res.data) {
				const top5 = res.data.slice(0, 5);
				setData(top5);
			}
		}
		getData();
	}, []);

	return (
		<div
			className='App'
			style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}
		>
			<div
				style={{ width: 500, height: 500, background: 'white', borderRadius: 5, color: 'white' }}
			>
				<h1
					style={{
						background: 'green',
						margin: 0,
						borderTopRightRadius: 5,
						borderTopLeftRadius: 5,
						padding: '10px 0',
					}}
				>
					Leaderboard
				</h1>
				<div style={{ color: 'black', padding: '10px 20px' }}>
					<div
						style={{
							display: 'flex',
							justifyContent: 'space-between',
							marginTop: 10,
							paddingBottom: 10,
							borderBottom: '1px solid lightgrey',
						}}
					>
						<div>
							<span style={{ marginRight: 100 }}>#</span>
							<span>Name</span>
						</div>
						<span>Number of reports</span>
					</div>
					{data.map((user, index) => {
						return (
							<div
								key={index}
								style={{ display: 'flex', justifyContent: 'space-between', marginTop: 30 }}
							>
								<div>
									<span style={{ marginRight: 100 }}>{index + 1}</span>
									<span style={{ textTransform: 'capitalize' }}>{user.fullName}</span>
								</div>
								<span>{user.reportCount} reports</span>
							</div>
						);
					})}
				</div>
			</div>
		</div>
	);
}

export default App;
