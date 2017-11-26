'use strict';

/**
 * @class HotelsService
 * @desc Object for handling api requests for the Hotel Component
 * @param {String} serverPort
 * @param {Map<Object<placeName: string, callbackFuncList: Array<Function>}
 */
class HotelsService {
    /**
     * @constructor HotelsService
     * @param {String} serverPort
     * @param {Map<Object<placeName: string, callbackFuncList: Array<Function>} placeMap
     * @param {Integer} updateIntervalTime
     */
    constructor(serverPort) {
        this.serverPort = serverPort;
        this.placeMap = new Map();
        this.updateIntervalTime = 4000;//Set to 4 seconds to have the content refreshed rougly by 5 seconds time
        //Initiate interval calls to the server to update hotel components if any have been added
        this.setPlaceUpdateInterval();
    }

     /**
     * @desc Adds the place with a callback function to be executed when the place updates
     * @param {String}placeName
     * @param {Function} callbackFunc
     */
    addPlaceAndCallbackFuncForIntervalUpdates(placeName, callbackFunc)
    {
        //Checks if the place has already been stored
        if (this.placeMap.has(placeName)) {
            //Add the callback function to the exisiting array of callback function to be executed when the place updates
            const callbackFuncList = this.placeMap.get(placeName);
            callbackFuncList.push(callbackFunc);
            this.placeMap.set(placeName, callbackFuncList)
        }
        else {
            //Add a record for the map with a new array containing the first call back function
            this.placeMap.set(placeName, [callbackFunc]);
        }
    }

    /**
     * @desc Makes Post Request for Places
     * @param {Array<String>} places
     */
    fetchPlaces(placeNames) {
        const requestBody = JSON.stringify({ placeNames });
        const requestHeaders = new Headers();
        requestHeaders.append('Content-Type', 'application/json; charset=utf-8');
        const requestOptions = {
            method: 'POST',
            headers: requestHeaders,
            body: requestBody
        };
        const request = new Request(`http://localhost:${this.serverPort}/Place`, requestOptions);
        //Execute request to GetPlaces endpoint
        return fetch(request).then((response) => {
            //Check if request was successful
            if (response.ok && response.status === 200) return response.json();
            return null;
        }).then((places) => {
            return places;
            }).catch(error => {
                //TODO: Log this error
                console.log('Error occurred fetching Places : ',error);
                return null;
            });
    };

    /**
     * @desc Sets an interval to execute every x seconds
     */
    setPlaceUpdateInterval()
    {
        setInterval(async () => {

            if (this.placeMap.size < 1) return;

            //Fetch latest hotels for the place names stored
            const places = await this.fetchPlaces([...this.placeMap.keys()]);

            //Check if places where retrieved
            if (!places || places.length < 1) return;

            //Get all the component callback functions to send components the new hotels
            const callbackFuncArray = this.getListOfComponentCallBacks();

            //Excute those callbacks with the new places so I can go to bed:)
            this.executeComponentHotelsUpdateCallbacks(callbackFuncArray, places);
            
        }, this.updateIntervalTime);
    }
    /**
     * @desc Get array of callback functions from places maps
     * @returns {Array<Functions>}
     */
    getListOfComponentCallBacks()
    {
        //Loop through callback functions to notify listeners
        const callbackFuncIterable = this.placeMap.values();

        //Check that there are callback functions
        if (!callbackFuncIterable) return;

        //Create array of all callback functions for components
        const callbackFuncArray = [];

        //Create array of callback functions
        for (let callBackFunc of callbackFuncIterable)
        {
            callbackFuncArray.push(...callBackFunc);
        }
        return callbackFuncArray;
    }

     /**
     * @desc Executes all the callback functions of components listening for the new Places with hotels
     * @param {Array<Functions>} callbackFuncArray
     * @param {Array<Place>} places
     */
    executeComponentHotelsUpdateCallbacks(callbackFuncArray, places)
    {
        //Check if there are any component functions to execute
        if (!callbackFuncArray || callbackFuncArray.length < 1) return;

        //Loop through the component functions and execute them with the new hotels
        const callbackFuncArrayLength = callbackFuncArray.length;
        for (let i = 0; i < callbackFuncArrayLength; i++)
        {
            const callbackFunction = callbackFuncArray[i];
            //Execute component function with new hotels
            callbackFunction(places);
        }
    }
}
export default HotelsService;