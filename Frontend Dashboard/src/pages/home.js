import React, { useState, useEffect } from 'react';
import { Layout, Menu, Breadcrumb, Spin, Avatar, Dropdown } from 'antd';
import {
	HomeOutlined,
	SettingOutlined,
	LogoutOutlined,
	PicCenterOutlined,
} from '@ant-design/icons';
import HomePanel from '../components/panels/HomePanel';
import SettingPanel from '../components/panels/SettingPanel';
import ReportPanel from '../components/panels/ReportPanel';
import api from '../utils/api';

const { Content, Footer, Sider, Header } = Layout;

const panes = [
	{
		name: 'Home',
		componenent: <HomePanel />,
		icon: <HomeOutlined />,
	},
	{
		name: 'Manage reports',
		componenent: <ReportPanel />,
		icon: <PicCenterOutlined />,
	},
];

export const AuthContext = React.createContext();

export default function HomePage() {
	const [collapsed, setCollapsed] = useState(false);
	const [paneIndex, setPaneIndex] = useState(0);
	const [curUser, setCurUser] = useState(null);
	const [isLoading, setIsLoading] = useState(true);

	useEffect(() => {
		async function fetchCurrentUser() {
			const token = localStorage.getItem('unibean-token');
			if (token != null) {
				const user = await api.get(`/api/users/token?token=${token}`);
				setCurUser(user.data);
				setIsLoading(false);
				return;
			}

			localStorage.clear();
			window.location = '/auth';
		}

		fetchCurrentUser();
	}, []);

	if (isLoading) {
		return (
			<div
				style={{
					background: 'rgba(0, 0, 0, 0.01)',
					width: '100vw',
					height: '100vh',
					display: 'flex',
					justifyContent: 'center',
					alignItems: 'center',
				}}
			>
				<Spin size='large' />
			</div>
		);
	}

	const logout = () => {
		localStorage.clear();
		window.location = '/auth';
	};

	const currentPane =
		paneIndex === -1
			? {
					name: 'Setting',
					componenent: <SettingPanel />,
					icon: <SettingOutlined />,
			  }
			: panes[paneIndex];

	return (
		<AuthContext.Provider value={{ curUser, setCurUser }}>
			<Layout style={{ minHeight: '100vh' }}>
				<Sider collapsible collapsed={collapsed} onCollapse={setCollapsed}>
					<div
						style={{
							height: '32px',
							margin: '16px',
							display: 'flex',
							alignItems: 'center',
							justifyContent: 'center',
						}}
					>
						{collapsed ? (
							<img src='/logo150.png' alt='Animal Protect logo' width='32' height='32' />
						) : (
							<span style={{ color: 'white', fontWeight: 'bold' }}>ADMIN DASHBOARD</span>
						)}
					</div>
					<Menu theme='dark' mode='inline' selectedKeys={paneIndex.toString()}>
						{panes.map((pane, index) => {
							return (
								<Menu.Item key={index} icon={pane.icon} onClick={() => setPaneIndex(index)}>
									{pane.name}
								</Menu.Item>
							);
						})}
						<Menu.Item key={-2} icon={<LogoutOutlined />} onClick={logout}>
							Logout
						</Menu.Item>
					</Menu>
				</Sider>
				<Layout>
					<Header
						style={{
							padding: 0,
							background: '#fff',
							display: 'flex',
							justifyContent: 'space-between',
						}}
					>
						<div />
						<Dropdown
							overlay={
								<Menu>
									<Menu.Item key={'2'} onClick={logout}>
										Log out
									</Menu.Item>
								</Menu>
							}
							trigger={['click']}
							placement='bottomLeft'
						>
							<div
								style={{
									display: 'flex',
									alignItems: 'center',
									margin: '0 16px',
									cursor: 'pointer',
								}}
							>
								<h4 style={{ marginRight: 15 }}>Hi {curUser.fullName}</h4>
								<Avatar
									size='large'
									src='https://i.pinimg.com/originals/34/60/3c/34603ce8a80b1ce9a768cad7ebf63c56.jpg'
								/>
							</div>
						</Dropdown>
					</Header>
					<Content style={{ margin: '0 16px' }}>
						{paneIndex !== 0 ? (
							<Breadcrumb style={{ margin: '16px 0' }}>
								<Breadcrumb.Item style={{ cursor: 'pointer' }} onClick={() => setPaneIndex(0)}>
									Home
								</Breadcrumb.Item>
								<Breadcrumb.Item>{currentPane.name}</Breadcrumb.Item>
							</Breadcrumb>
						) : (
							<div style={{ margin: '16px 0' }} />
						)}
						{currentPane.componenent}
					</Content>
					<Footer style={{ textAlign: 'center' }}>
						©2021 by Animal Protect - ZooHackathon Vietnam
					</Footer>
				</Layout>
			</Layout>
		</AuthContext.Provider>
	);
}
