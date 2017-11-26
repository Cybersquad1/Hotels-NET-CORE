'use strict';

/**
 * @class HotelComponent
 * @property {Hotel} hotel
 * @property {Element} containerElement
 * @property {Element} componentElement
 * @property {Element} hotelImgElement
 * @property {Element} hotelNameDisplayElement
 * @property {Element} hotelStarsElement
 * @property {Element} hotelRateDisplayElement
 */
class HotelComponent {
     /**
     * @constructor HotelComponent
     * @param {Hotel} hotel
     * @param {Element} containerElement
     */
    constructor(hotel, containerElement) {
        //Check that valid dependencies were injected
        if (!containerElement || !hotel ) throw new Error('HotelComponent failed to create : Invalid constructor parameters.');

        //Create component element
        this.containerElement = containerElement;
        this.componentElement = document.createElement('div');
        this.componentElement.classList.add('hotel-component');
        this.setHotel(hotel);
        
    }
     /**
     * @desc Initialize HotelComponent child elements with data
     */
    initialize()
    {
        //Get component properties
        const { componentElement, hotel} = this;
        const { image, name, starRating, rate } = hotel;
        //Create component child elements and add them
        this.createHotelImgElement(image, componentElement);
        this.createHotelNameDisplayElement(name, componentElement);
        this.createHotelStarsElement(starRating, componentElement);
        this.createHotelRateDisplayElement(rate, componentElement);
        this.containerElement.appendChild(componentElement)
    }
     /**
     * @desc Create Hotel Image element and append it to the component
     * @param {String} image
     * @param {Element} componentElement
     */
    createHotelImgElement(image, componentElement)
    {
        this.hotelImgElement = document.createElement('img');
        this.hotelImgElement.classList.add('hotel-img');
        this.hotelImgElement.alt = "No Image";
        this.setHotelImgElement(image);
        componentElement.appendChild(this.hotelImgElement);
    }
     /**
     * @desc Create Hotel Name Display element and append it to the component
     * @param {String} name
     * @param {Element} componentElement
     */
    createHotelNameDisplayElement(name, componentElement)
    {
        this.hotelNameDisplayElement = document.createElement('span');
        this.hotelNameDisplayElement.classList.add('hotel-name');
        this.setHotelNameDisplayElement(name);
        componentElement.appendChild(this.hotelNameDisplayElement);
    }
     /**
     * @desc Create Hotel Stars Rating element and append it to the component
     * @param {Integer} starRating
     * @param {Element} componentElement
     */
    createHotelStarsElement(starRating, componentElement)
    {
        this.createHotelStarsElement = document.createElement('div');
        this.createHotelStarsElement.classList.add('hotel-stars');
        this.setHotelStarsElement(starRating);
        componentElement.appendChild(this.createHotelStarsElement);
    }
     /**
     * @desc Create Hotel Stars Rate Display element and append it to the component
     * @param {Decimal} rate
     * @param {Element} componentElement
     */
    createHotelRateDisplayElement(rate, componentElement)
    {
        this.hotelRateDisplayElement = document.createElement('span');
        this.hotelRateDisplayElement.classList.add('hotel-rate');
        this.setHotelRateDisplayElement(rate);
        componentElement.appendChild(this.hotelRateDisplayElement);
    }
   /**
    * @desc Set Hotel data
    * @param {Hotel} hotel
    */
    setHotel(hotel)
    {
        this.hotel = hotel;
    }
   /**
    * @desc Set Hotel Image element image asynchronously
    * @param {String} image
    */
    setHotelImgElement(image) {

        if (image)
        {
            const self = this;
            var downloadingImage = new Image();
            downloadingImage.onload = function () {
                self.hotelImgElement.src = this.src;
            };
            downloadingImage.src = `../images/Hotels/${image}`;
        }
        else
        {
            this.hotelImgElement.src = '';
        }
       
    }
    /**
    * @desc Set Hotel Name Display element
    * @param {String} name
    */
    setHotelNameDisplayElement(name) {
        this.hotelNameDisplayElement.textContent = name;
    }
    /**
    * @desc Create Hotel Rating element and append it to the component
    * @param {Integer} starRating
    */
    setHotelStarsElement(starRating) {
        const ratedStars = starRating;
        const unratedStars = 5 - starRating;
        let starsHtmlString = '';

        //Construct number of stars rated html string
        for (let i = 0; i < ratedStars; i++) {
            starsHtmlString += '<img src="../images/star.png" />';
        }

        //Construct number of stars not rated html string
        for (let i = 0; i < unratedStars; i++) {
            starsHtmlString += '<img src="../images/star_grey.png" />';
        }
        this.createHotelStarsElement.innerHTML = starsHtmlString
    }
    /**
    * @desc Set Hotel Stars Rate Display element
    * @param {Decimal} rate
    */
    setHotelRateDisplayElement(rate) {
        this.hotelRateDisplayElement.textContent = `From $ ${rate}`;
    }
    /**
    * @desc Update Hote Component with new Hotel
    * @param {Hotel} hotel
    */
    update(hotel) {
        this.updateHotelImgElement(hotel.image);
        this.updateHotelNameDisplayElement(hotel.name);
        this.updateHotelStarsElement(hotel.starRating);
        this.updateHotelRateDisplayElement(hotel.rate);
        this.setHotel(hotel);
    }
    /**
     * @desc Update Hotel Image element if the image has changed
     * @param {String} image
    */
    updateHotelImgElement(image) {
        if (image === this.hotel.image) return;
        this.setHotelImgElement(image);
    }
    /**
     * @desc Update Hotel Name element if the name has changed
     * @param {String} name
    */
    updateHotelNameDisplayElement(name) {
        if (name === this.hotel.name) return;
        this.setHotelNameDisplayElement(name);
    }
    /**
    * @desc Update Hotel Stars Rating element if the amount of stars have changed
    * @param {Integer} starRating
    */
    updateHotelStarsElement(starRating) {
        if (starRating === this.hotel.starRating) return;
        this.setHotelStarsElement(starRating);
    }
    /**
   * @desc Update Hotel Rate Display element if the rate has changed
   * @param {Decimal} rate
   */
    updateHotelRateDisplayElement(rate) {
        if (rate === this.hotel.rate) return;
        this.setHotelRateDisplayElement(rate);
    }
}
export default HotelComponent;
