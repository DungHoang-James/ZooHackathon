import { useContext } from 'react';
import { Input, Button, Form, message } from 'antd';
import { AuthContext } from '../../pages/home';
import api from '../../utils/api';

export default function SettingPanel() {
	const { curUser, setCurUser } = useContext(AuthContext);

	const onUpdate = async (values) => {
		try {
			const res = await api.patch(`/api/auth/me`, values);
			setCurUser(res.data.data);
			message.success('Update successful!');
		} catch (error) {
			message.error(error.response.data.message || error.message);
		}
	};

	return (
		<div style={{ padding: 24, minHeight: 360, background: '#fff' }}>
			<div>
				<Form
					autoComplete='off'
					onFinish={onUpdate}
					initialValues={curUser}
					labelCol={{ span: 3 }}
					wrapperCol={{ span: 10 }}
				>
					<Form.Item label='ID' name='id'>
						<Input disabled />
					</Form.Item>
					<Form.Item label='GoogleID' name='googleId'>
						<Input disabled />
					</Form.Item>
					<Form.Item label='Role' name='role'>
						<Input disabled />
					</Form.Item>
					<Form.Item label='Email' name='email'>
						<Input disabled />
					</Form.Item>
					<Form.Item
						label='Name'
						name='name'
						rules={[{ required: true, message: 'Please input name!' }]}
					>
						<Input />
					</Form.Item>
					<Form.Item wrapperCol={{ offset: 3, span: 10 }}>
						<Button type='primary' htmlType='submit'>
							Update
						</Button>
					</Form.Item>
				</Form>
			</div>
		</div>
	);
}
