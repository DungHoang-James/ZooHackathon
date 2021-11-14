import './App.css';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Home from './pages/home';
import Auth from './pages/auth';
import NotFound from './pages/404';

function App() {
	return (
		<Router>
			<Switch>
				<Route exact path='/auth' component={Auth} />
				<Route exact path='/' component={Home} />
				<Route exact path='*' component={NotFound} />
			</Switch>
		</Router>
	);
}

export default App;
