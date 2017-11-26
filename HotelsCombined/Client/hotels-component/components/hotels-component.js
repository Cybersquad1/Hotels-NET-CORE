'use strict';

/**
 * @class HotelsComponent
 * @property {Element} componentElement
 * @property {HotelsHeaderComponent} hotelsHeaderComponent
 * @property {String} placeName
 * @property {HotelsService} hotelsService
 * @property {Constructor<HotelsHeaderComponent>} HotelsHeaderComponent
 * @property {Constructor<HotelsViewComponent>} HotelsViewComponent
 * @property {Constructor<HotelComponent>} HotelComponent
 * @property {HotelComponentsStore} hotelComponentsStore
 */
class HotelsComponent {
     /**
     * @constructor HotelsComponent
     * @param {Element} containerElement
     * @param {String} placeName
     * @param {HotelsService} hotelsService
     * @param {Constructor<HotelsHeaderComponent>} HotelsHeaderComponent
     * @param {Constructor<HotelsViewComponent>} HotelsViewComponent
     * @param {Constructor<HotelComponent>} HotelComponent
     * @param {HotelComponentsStore} hotelComponentsStore
     */
    constructor(containerElement, placeName, hotelsService, HotelsHeaderComponent, HotelsViewComponent, HotelComponent, hotelComponentsStore) {
        //Check that valid dependencies were injected
        if (!containerElement ||
            !placeName ||
            !hotelsService ||
            !HotelsHeaderComponent ||
            !HotelsViewComponent ||
            !HotelComponent ||
            !hotelComponentsStore) throw new Error('HotelsComponent failed to create : Invalid constructor parameters.');

         //Set properties
        this.componentElement = containerElement;
        this.componentElement.classList.add('hotels-component');
        this.hotelsHeaderComponent = null;
        this.placeName = placeName;
        this.hotelsService = hotelsService;
        this.HotelsHeaderComponent = HotelsHeaderComponent;
        this.HotelsViewComponent = HotelsViewComponent;
        this.HotelComponent = HotelComponent;
        this.hotelComponentsStore = hotelComponentsStore;
        this.createErrorMessageElement();
    }
     /**
     * @desc Initialize HotelsComponent: Getting data and building child components
     */
    async initialize()
    {
        //Get all the necessary property values to construct the component
        const { hotelsService, placeName, componentElement, refreshClickHandler, HotelsHeaderComponent, HotelsViewComponent, HotelComponent, hotelComponentsStore, errorMessage} = this;

        //Fetch place from server
        const places = await hotelsService.fetchPlaces([placeName]);

        //Check if data was retrieved
        if (places && places.length > 0)
        {
            //Get the single place retrieved
            const place = places[0];

            //Create Component Header
            this.hotelsHeaderComponent = new HotelsHeaderComponent(componentElement, place, refreshClickHandler.bind(this));
            this.hotelsHeaderComponent.initialize();

            if (place.hotels)
            {
                //Create Component Content
                this.hotelsViewComponent = new HotelsViewComponent(place.hotels, componentElement, HotelComponent);

                //Initialise component
                this.hotelsViewComponent.initialize();

                //Store HotelComponents
                hotelComponentsStore.save(this.hotelsViewComponent.hotelComponents);
            }
            else {
                //Display error message
                componentElement.appendChild(errorMessage);
            }
            
        }
        else {
           //Display error message
            componentElement.appendChild(errorMessage);
        }
        //Add place and callback method for next interval request to update the hotels
        hotelsService.addPlaceAndCallbackFuncForIntervalUpdates(placeName, this.updatePlacesIntervalCallback.bind(this));
    }
     /**
     * @desc Refresh HotelsComponent data and child components with that data
     */
    async refreshClickHandler()
    {
        const { placeName, hotelsService } = this;
        //Fetch latest place from server
        const places = await hotelsService.fetchPlaces([placeName]);
        if (!places || places.length < 1) return;
        const place = places[0];
        //Update component with latest hotels
        this.updatPlaceHotels(place);
    }
    /**
     * @desc Refresh HotelsComponent data and child components with that data
     * @param {Place} place
     */
    updatPlaceHotels(place)
    {  
        if (place && place.hotels) {
            //Update Store HotelComponents
            this.hotelComponentsStore.update(place.hotels);
        }
    }
     /**
     * @desc Function to be executed when the interval request for the latest hotels finishes and receives new places
     * @param {Array<Place>} places
     */
    updatePlacesIntervalCallback(places)
    {
        //Check if places were retrieved
        if (!places || places.length < 1) return;

        //Loop through places to find the hotels linked to the place of this component
        const placesLength = places.length;
      
        for (let i = 0; i < placesLength; i++)
        {
            const place = places[i];

            //Check if place names match
            const componentPlaceName = this.getComponentPlaceNameWithSpecialCharsRemoved();
            if (place.name === componentPlaceName)
            {
                //Update component
                this.updatPlaceHotels(place);
                return;
            }   
        }
    }
    /**
     * @desc Server often requires place names with special chars like "_" for spaces so this method removes them
     */
    getComponentPlaceNameWithSpecialCharsRemoved()
    {
        return this.placeName.replace(/_/g, ' ')
    }
    /**
     * @desc Create default error message for when shit hits north, not south because all the good stuff happens south
     */
    createErrorMessageElement()
    {
        this.errorMessage = document.createElement('h1');
        this.errorMessage.textContent = "Sorry.Content failed to load."
    }
}
export default HotelsComponent;
