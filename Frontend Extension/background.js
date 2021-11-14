let autoCheck = false;
let currentUser = null;
let deviceId = uuidv4();

chrome.runtime.onInstalled.addListener(async () => {
	await chrome.storage.sync.set({ autoCheck, currentUser, deviceId });
});

function uuidv4() {
	return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, (c) =>
		(c ^ (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))).toString(16)
	);
}


// 4e73c7c7-894d-43ff-bb2e-c68e2f75b895