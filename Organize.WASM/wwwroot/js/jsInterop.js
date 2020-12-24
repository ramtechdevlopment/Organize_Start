window.addEventListener('resize',
    () => {
        //Call the static Method OnResize in the Organize assembly
        console.log("Static Resize from js");
        DotNet.invokeMethodAsync("Organize.WASM", "OnResize");
    });

window.blazorDimension = {
    getWidth: () => window.innerWidth
}

