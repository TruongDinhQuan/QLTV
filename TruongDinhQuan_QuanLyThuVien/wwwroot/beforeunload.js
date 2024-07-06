window.registerBeforeUnloadHandler = function () {
    window.addEventListener('beforeunload', function (event) {
        fetch('/logout', { method: 'POST' });
    });
};

