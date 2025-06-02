window.resizeHelper = {
    registerResizeHandler: function (dotNetHelper) {
        function checkSize() {
            const width = window.innerWidth;
            dotNetHelper.invokeMethodAsync('OnResize', width);
        }

        // Fire once at registration
        checkSize();

        // Listen for resize events
        window.addEventListener('resize', checkSize);
    }
};