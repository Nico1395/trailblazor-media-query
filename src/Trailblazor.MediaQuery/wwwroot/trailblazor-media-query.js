window.MediaQueryInstanceManagerJs = {
    _instances: {},

    register(instanceId, breakpoints, mediaQueryInstanceManagerDotnet) {
        if (this._instances[instanceId])
            return;

        this._instances[instanceId] = [];

        breakpoints.forEach(bp => {
            const mediaQueryList = window.matchMedia(bp.queryString);
            const callback = () => mediaQueryInstanceManagerDotnet.invokeMethodAsync('NotifyBreakpointChange', instanceId, bp.key, mediaQueryList.matches);

            mediaQueryList.addEventListener('change', callback);
            this._instances[instanceId].push({ mq: mediaQueryList, callback });

            callback();
        });
    },

    unregister(instanceId) {
        const entries = this._instances[instanceId];
        if (!entries)
            return;

        entries.forEach(({ mq, callback }) => mq.removeEventListener('change', callback));
        delete this._instances[instanceId];
    }
};
