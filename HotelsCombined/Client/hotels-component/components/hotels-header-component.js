'use strict';

/**
 * @class HotelsHeaderComponent
 * @property {Element} containerElement
 * @property {Element} componentElement
 * @property {Element} headerTextElement
 * @property {Element} refreshHotelsButton
 * @property {Place} place
 * @property {Function} refreshClickHandler
 */
class HotelsHeaderComponent {
     /**
     * @constructor HotelsHeaderComponent
     * @param {Element} containerElement
     * @param {Place} place
     * @param {Function} refreshClickHandler
     */
    constructor(containerElement, place, refreshClickHandler) {

        //Check that valid dependencies were injected
        if (!containerElement || !place || !refreshClickHandler) throw Error('HotelsHeaderComponent failed to create : Invalid constructor parameters.');

        //Set container element that the component will be appended to
        this.containerElement = containerElement;

        //Create component element
        this.componentElement = document.createElement('div');
        this.componentElement.classList.add("hotels-component-header");

        //Create child element for component
        this.headerTextElement = document.createElement('span')
        this.headerTextElement.classList.add("header-text");
        this.headerTextElement.textContent = `Hotels in ${place.name}`;

         //Create child element for component
        this.refreshHotelsButton = document.createElement('button')
        this.refreshHotelsButton.classList.add("refresh-button");
        this.refreshHotelsButton.innerHTML  = "Refresh";
        this.refreshHotelsButton.addEventListener('click', refreshClickHandler);

        //Set data for component
        this.place = place;
    }
     /**
     * @desc Initialize HotelsHeaderComponent child elements
     */
    initialize()
    {
        //Generate component element
        const { headerTextElement, refreshHotelsButton, componentElement, containerElement } = this;
        componentElement.appendChild(headerTextElement);
        componentElement.appendChild(refreshHotelsButton);
        containerElement.appendChild(componentElement);
    }
}
export default HotelsHeaderComponent;
