import React from 'react';
import { Column } from '@ant-design/charts';

const ReportColumn = () => {
	const data = [
		{
			type: '0% - 25%',
			sales: 10,
		},
		{
			type: '25% - 50%',
			sales: 20,
		},
		{
			type: '50% - 75%',
			sales: 100,
		},
		{
			type: '75% - 100%',
			sales: 200,
		},
	];

	const config = {
		data,
		xField: 'type',
		yField: 'sales',
		label: {
			position: 'middle',
			style: {
				fill: '#FFFFFF',
				opacity: 0.6,
			},
		},
		meta: {
			type: { alias: 'Category' },
			sales: { alias: 'Number of images' },
		},
	};
	return <Column {...config} />;
};

export default ReportColumn;
