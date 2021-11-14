import firebase from 'firebase/app';
import 'firebase/storage';

const firebaseConfig = {
	apiKey: 'AIzaSyBuP8UpUns5KfUczR05QxjptmuBIk5-01E',
	authDomain: 'fptu-unibean.firebaseapp.com',
	projectId: 'fptu-unibean',
	storageBucket: 'fptu-unibean.appspot.com',
	messagingSenderId: '991070428517',
	appId: '1:991070428517:web:38b6c1c0726591976ed58c',
	measurementId: 'G-5QMT6Q0MRE',
};

firebase.initializeApp(firebaseConfig);

const storage = firebase.storage();

export { storage, firebase as default };
