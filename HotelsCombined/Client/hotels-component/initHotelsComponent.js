'use strict';
import './styles/style.css';
import immutable from 'immutable'
import HotelsComponent from './components/hotels-component';
import HotelsHeaderComponent from './components/hotels-header-component';
import HotelsViewComponent from './components/hotels-view-component';
import HotelComponent from './components/hotel-component';
import HotelsService from './services/hotelsService';
import HotelComponentsStore from './stores/hotelComponentsStore';


/**
* @desc Initialise HotelComponent in its own scope
*/
(function () {
    try {
        //Get the parent element and data associated with the current executing script tag
        const { containerElement, placeName } = GetPlaceNameAndContainerElement();

        //Create service for retrieving hotels data from the server
        const hotelsService = GetGlobaclHoteService();

        //Create store for storing HotelComponents generated to reuse their objects and html later
        const hotelComponentsStore = new HotelComponentsStore(immutable)

        //Create new HotelsComponent
        const hotelsComponents = new HotelsComponent(containerElement, placeName, hotelsService, HotelsHeaderComponent, HotelsViewComponent, HotelComponent, hotelComponentsStore);

        hotelsComponents.initialize().then(() => { console.log(`${placeName} Hotels Component Loaded`) });

        /**
        * @desc Get the place name data and parent element of the current executing script tag
        * @returns <Object {containerElement: Element, placeName: String}>
        */
        function GetPlaceNameAndContainerElement() {
            const scripts = document.getElementsByTagName('script');
            const index = scripts.length - 1;
            const currentScript = scripts[index];
            const containerElement = currentScript.parentElement;
            const placeName = currentScript.getAttribute('data-place');
            return { containerElement, placeName };
        }

        /**
        * @desc Get the global hotel service for updating hotel components with server data
        */
        function GetGlobaclHoteService() {
            //Check if a valid global HotelService has been created
            if (window.hotelsService) return window.hotelsService;

            //Create new global hotel service
            window.hotelsService = new HotelsService(window.location.port);

            return window.hotelsService;
        }
    }
    catch (e)
    {
        console.log('Hotel Component Failed to load.', e);
    }
})();
