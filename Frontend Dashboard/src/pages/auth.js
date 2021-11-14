import { Typography, Form, Input, Button } from 'antd';
import { useEffect } from 'react';
import api from '../utils/api';

export default function AuthPage() {
	useEffect(() => {
		const token = localStorage.getItem('unibean-token');
		if (token != null) {
			window.location = '/';
		}
	}, []);

	const onFinish = async (values) => {
		const { email, password } = values;
		const res = await api.post(`/api/users/login?email=${email}&password=${password}`);

		if (res.data) {
			localStorage.setItem('unibean-token', res.data.token);
			window.location = '/';
		}
	};

	const onFinishFailed = (errorInfo) => {
		console.log('Failed:', errorInfo);
	};

	return (
		<div
			style={{
				width: '100vw',
				height: '100vh',
				backgroundImage: 'url(/background.jpg)',
				backgroundSize: 'cover',
				backgroundPosition: 'center center',
				display: 'flex',
				alignItems: 'center',
			}}	
		>
			<div style={{ padding: '20 10', marginLeft: 200, textAlign: 'center' }}>
				<img alt='logo' src='/logo150.png' className='app-logo' style={{ marginBottom: 20 }} />
				<Typography.Title level={3} style={{ marginBottom: 50 }}>
					Animal Protect For Admin
				</Typography.Title>

				<Form
					name='basic'
					labelCol={{ span: 6 }}
					wrapperCol={{ span: 16 }}
					initialValues={{ remember: true }}
					onFinish={onFinish}
					onFinishFailed={onFinishFailed}
					autoComplete='off'
				>
					<Form.Item label='Email' name='email'>
						<Input size='large' />
					</Form.Item>

					<Form.Item label='Password' name='password'>
						<Input.Password size='large' />
					</Form.Item>

					<Form.Item wrapperCol={{ offset: 4, span: 10 }}>
						<Button type='primary' htmlType='submit'>
							Login
						</Button>
					</Form.Item>
				</Form>
			</div>
		</div>
	);
}
