import { tns } from "tiny-slider";

window.tinySlider = (args) => {
  const tinyInterval = setInterval(() => {
    const { container } = args;
    let element = "";
    if ((container && container.startsWith(".")) || container.startsWith("#"))
      element = document.getElementsByClassName(container.substr(1));
    else element = document.getElementsByClassName(container);

    if (element.length > 0) {
      clearInterval(tinyInterval);

      tns(args);
    }
  }, 100);
};
