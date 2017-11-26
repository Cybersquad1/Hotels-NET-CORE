/**
 * RECONFIGURE THIS FILE TO COMPILE WHICH EVER COMPONENT YOU WANT
*/
const webpack = require('webpack');
const path = require('path');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

const ExtractTextPluginConfig = new ExtractTextPlugin('hotels-widget.css');
// Configuration for client-side bundle suitable for running in browsers
const clientBundleOutputDir = '../wwwroot/lib/widgets';
module.exports = {
     //COMMENT THIS LINE OUT IF YOU DON'T WANT TO PRODUCE A PRODUCTION READY COMPONENT
    //devtool: 'source-map',
    context: __dirname,
    entry: './hotels-component/initHotelsComponent',
    output: {
        path: path.join(__dirname, clientBundleOutputDir),
        filename: 'hotels-widget.js'
    },
    module: {
        rules: [{
            test: /\.css$/,
            use: ExtractTextPluginConfig.extract({
                use: [{
                    loader: 'css-loader'
                }]
            })
        },{
            test: /\.js$/,
            exclude: /node_modules/,
            loader: 'babel-loader',
            options: {
                presets: ['env', 'stage-2'],
                plugins: [
                    'syntax-async-functions',
                    'transform-regenerator',
                    'transform-object-rest-spread',
                    'transform-runtime'
                ]
            }
          }
        ]
    },
    plugins: [
        ExtractTextPluginConfig
         //COMMENT THIS LINE OUT IF YOU DON'T WANT TO PRODUCE PRODUCTION READY COMPONENT
       ,new webpack.optimize.UglifyJsPlugin()
    ]
};
