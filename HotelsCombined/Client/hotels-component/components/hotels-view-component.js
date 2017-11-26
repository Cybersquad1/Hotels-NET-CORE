'use strict';

/**
 * @class HotelsViewComponent
 * @property {Array<Hotel>} hotels
 * @property {Element} containerElement
 * @property {Element} componentElement
 * @property {Constructor<HotelComponent>} HotelComponent
 */
class HotelsViewComponent {
    constructor(hotels, containerElement, HotelComponent) {
        //Check that valid dependencies were injected
        if (!hotels || !containerElement || !HotelComponent) throw Error('HotelsViewComponent failed to create : Invalid constructor parameters.');

        //Set properties
        this.containerElement = containerElement;
        this.componentElement = document.createElement('div');
        this.componentElement.classList.add('hotels-component-view');
        this.HotelComponent = HotelComponent;
        this.hotels = hotels;
        this.hotelComponents = [];
    }
     /**
     * @desc Initialize HotelsViewComponent child elements with data
     */
    initialize()
    {
        //Get all necessary properties to create child components
        const { containerElement, componentElement, HotelComponent, hotels, hotelComponents } = this;
        //Create document fragement to avoid DOM thrashing when loading HotelComponents
        const tempHotelViewComponentDocFragment = document.createDocumentFragment();
        //Loop through hotels and create components for each one
        const hotelsLength = hotels.length;
        for (let i = 0; i < hotelsLength; i++)
        {
            const hotel = hotels[i];
            const hotelComponent = new HotelComponent(hotel, tempHotelViewComponentDocFragment);
            hotelComponent.initialize();
            //Add to list of hotel components
            hotelComponents.push(hotelComponent);
        }
        componentElement.appendChild(tempHotelViewComponentDocFragment);
        containerElement.appendChild(componentElement);
    }
}
export default HotelsViewComponent;
