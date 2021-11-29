const frisby = require('frisby');
const { API_ENDPOINT } = require('../config');
const Joi = frisby.Joi;

describe('Companies', () => {
    it('should return a list of companies', function () {
        return frisby
            .get(`${API_ENDPOINT}/api/Companies`)
            .expect('status', 200)
            .expect('jsonTypes', '*', {
                id: Joi.number().required(),
                name: Joi.string().required(),
                creationDate: Joi.string().allow(null)
            });
    });
});