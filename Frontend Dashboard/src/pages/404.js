import { Result, Button } from 'antd';

export default function NotFoundPage() {
	const backHome = () => {
		window.location = '/';
	};

	return (
		<div
			style={{
				display: 'flex',
				justifyContent: 'center',
				alignItems: 'center',
				width: '100vw',
				height: '100vh',
				background: 'rgba(0, 0, 0, 0.01)',
			}}
		>
			<Result
				status='404'
				title='404'
				subTitle='Sorry, the page you visited does not exist.'
				extra={
					<Button type='primary' onClick={backHome}>
						Back Home
					</Button>
				}
			/>
		</div>
	);
}
