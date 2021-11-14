import React from 'react';
import { Pie } from '@ant-design/charts';

const UserPie = ({ registered, guest }) => {
	var data = [
		{
			type: 'Registered',
			value: registered,
		},
		{
			type: 'Guest',
			value: guest,
		},
	];
	var config = {
		appendPadding: 10,
		data: data,
		angleField: 'value',
		colorField: 'type',
		radius: 0.8,
		legend: false,
		label: {
			type: 'outer',
			content: '{name} {percentage}',
		},
		interactions: [{ type: 'pie-legend-active' }, { type: 'element-active' }],
	};
	return <Pie {...config} />;
};

export default UserPie;
