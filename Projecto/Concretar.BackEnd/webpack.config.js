var webpack = require('webpack');
var path = require('path');
var min = process.env.NODE_ENV === "production"; //or set equal to true to uglify + minimize bundle javascript and css documents.
var config = {
    entry: './wwwroot/source/index.js',
    output: {
        path: path.resolve(__dirname, 'wwwroot/dist'),
        filename: 'webpack.bundle.js',
    },
    module: {
        loaders: [
            { test: /\.css$/, loader: "style-loader!css-loader" + (min ? '?minimize' : '') },
            { test: /\.scss$/, loader: "style-loader!css-loader!sass-loader" + (min ? '?minimize' : '') },
            //{ test: /\.html$/, loader: "html-loader" },
        ]
    },
    plugins: [
      /*new HtmlWebPackPlugin({
      template: 'src/index.html'
    }),*/
    /*new CleanWebPackPlugin(['dist'])*/
    ],
    watch: true
};
if (min) {
    config.plugins.push(new webpack.optimize.UglifyJsPlugin({
        minimize: true
    }));
}
module.exports = config;
