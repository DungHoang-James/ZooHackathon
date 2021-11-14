let autoCheck = false;
let currentUser = null;

chrome.runtime.onInstalled.addListener(async () => {
	await chrome.storage.sync.set({ autoCheck, currentUser });
});
