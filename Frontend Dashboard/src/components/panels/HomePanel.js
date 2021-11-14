import { Statistic, Card, Row, Col, Spin } from 'antd';
import { useCallback, useEffect, useState } from 'react';
import api from '../../utils/api';
import UserPie from '../chart/UserPie';
import ReportColumn from '../chart/ReportColumn';

export default function HomePanel() {
	const [statistic, setStatistic] = useState();
	const [isLoading, setIsLoading] = useState(true);

	const onLoad = useCallback(async () => {
		setIsLoading(true);
		const res = await api.get(`/api/statistics`);
		if (res && res.data) {
			setStatistic(res.data);
		}
		setIsLoading(false);
	}, []);

	useEffect(() => onLoad(), [onLoad]);

	if (isLoading)
		return (
			<div
				style={{
					width: '100%',
					height: '100%',
					display: 'flex',
					justifyContent: 'center',
					alignItems: 'center',
				}}
			>
				<Spin size='large' />
			</div>
		);

	const result = [
		{
			title: 'Number of users',
			value: statistic.totalUser + statistic.totalDevice,
		},
		{
			title: 'Number of registered users',
			value: statistic.totalUser,
		},
		{
			title: 'Total reports',
			value: statistic.totalReport,
		},
		{
			title: 'Average accuracy of all reports',
			value: `${statistic.averagePercentCorrect}%`,
		},
	];

	const getColor = (index) => {
		if (index <= 2) {
			return '#1890ff';
		}
		if (index <= 5) {
			return '#52c41a';
		}
		if (index <= 8) {
			return '#faad14';
		}
		return '#ff4d4f';
	};

	return (
		<div style={{ padding: 24 }}>
			<Row gutter={[24, 24]}>
				{result
					.filter((card) => card.value != null)
					.map((card, index) => (
						<Col key={index} span={8}>
							<Card>
								<Statistic valueStyle={{ color: getColor(index) }} {...card} />
							</Card>
						</Col>
					))}
			</Row>
			<Row gutter={[24, 24]} style={{ marginTop: 50 }}>
				<Col span={10}>
					<UserPie registered={45} guest={75} />
					<div style={{ textAlign: 'center', marginTop: 10 }}>User status chart</div>
				</Col>
				<Col span={14}>
					<ReportColumn />
					<div style={{ textAlign: 'center', marginTop: 10 }}>Accuracy statistics chart</div>
				</Col>
			</Row>
		</div>
	);
}
