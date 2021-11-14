import React, { useEffect, useState } from 'react';
import { Table, Typography, Input, Image } from 'antd';
import api from '../../utils/api';

const HeaderTable = () => {
	return (
		<div style={{ display: 'flex', justifyContent: 'space-between' }}>
			<Typography.Title level={5}>Reports Management</Typography.Title>
			<div>
				<Input.Search placeholder='Search reports' style={{ width: 200, marginBottom: '20px' }} />
			</div>
		</div>
	);
};

export default function ReportPanel() {
	const [data, setData] = useState([]);
	const [isLoading, setIsLoading] = useState(true);

	const onLoad = async () => {
		setIsLoading(true);
		const res = await api.get(`/api/reports`);
		if (res && res.data) {
			setData(res.data);
		}
		setIsLoading(false);
	};

	const columns = [
		{
			title: 'ID',
			dataIndex: 'id',
			width: '5%',
			sorter: (a, b) => a.id - b.id,
		},
		{
			title: 'User ID',
			dataIndex: 'userID',
			width: '10%',
		},
		{
			title: 'Device ID',
			dataIndex: 'deviceID',
			width: '10%',
		},
		{
			title: 'Created at',
			dataIndex: 'createDate',
			width: '10%',
			render: (_, record) => (
				<div>
					{new Date(record.createDate).getDate()}-{new Date(record.createDate).getMonth() + 1}-
					{new Date(record.createDate).getFullYear()}
				</div>
			),
		},
		{
			title: 'Report Texts',
			dataIndex: 'reportTexts',
			width: '20%',
			render: (_, record) => (
				<div style={{ display: 'flex', flexWrap: 'wrap' }}>
					{record.reportTexts.map((data, index) => (
						<span
							style={{
								backgroundColor: 'lightgrey',
								padding: '5px 10px',
								marginRight: 5,
								marginBottom: 5,
								borderRadius: 10,
							}}
							key={index}
						>
							{data.text}
						</span>
					))}
				</div>
			),
		},
		{
			title: 'Report Images',
			dataIndex: 'reportImages',
			width: '20%',
			render: (_, record) => (
				<div>
					{record.reportImages.map((data, index) => (
						<div
							style={{
								display: 'flex',
								justifyContent: 'space-between',
								alignItems: 'center',
								marginBottom: 10,
								marginTop: 10,
							}}
							key={index}
						>
							<Image
								key={index}
								src={data.imageURL || '/placeholder.png'}
								style={{ width: '250px' }}
							/>
							<span
								style={{
									backgroundColor: 'lightgrey',
									padding: '5px 10px',
									borderRadius: 10,
									marginLeft: 10,
								}}
							>
								{data.percentCorrect}%
							</span>
						</div>
					))}
				</div>
			),
		},
	];

	useEffect(() => onLoad(), []);

	return (
		<div style={{ padding: 24, minHeight: 360, background: '#fff' }}>
			<div>
				<HeaderTable />
				<Table
					columns={columns}
					dataSource={data.map((record) => ({ ...record, key: record.id }))}
					loading={isLoading}
					summary={(pageData) => (
						<Table.Summary fixed>
							<Table.Summary.Row>
								<Table.Summary.Cell index={0} colSpan={10}>
									<Typography.Title level={5}>
										Show {pageData.length} of total {data.length} reports
									</Typography.Title>
								</Table.Summary.Cell>
							</Table.Summary.Row>
						</Table.Summary>
					)}
				/>
			</div>
		</div>
	);
}
