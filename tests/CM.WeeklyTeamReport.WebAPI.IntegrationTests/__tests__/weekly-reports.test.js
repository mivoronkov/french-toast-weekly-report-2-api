const frisby = require('frisby');
const { API_ENDPOINT } = require('../config');
const Joi = frisby.Joi;

describe('Weekly Reports', () => {
    //GET: API_ENDPOINT/api/Companies
    it('should return a list of weekly reports', function () {
        throw new Error();
        return frisby
            .get(`${API_ENDPOINT}/api/Companies/${1}/Members/${1}/Reports`)
            .expect('status', 200)
            .expect('jsonTypes', '*', {
                id: Joi.number().required(),
                firstName: Joi.string().required(),
                lastName: Joi.string().required(),
                title: Joi.string().required(),
                email: Joi.string().required(),
                sub: Joi.string().required(),
                companyId: Joi.number().required()
            });
    });

    it('should CRUD a weekly report', async () => {
        throw new Error();
        // Company setup
        let result = await frisby
            .post(`${API_ENDPOINT}/api/Companies`, {
                Name: 'Some model agency',
                CreationDate: new Date(Date.now()).toISOString()
            });
        const company = result.json;

        // Team member create
        result = await frisby
            .post(`${API_ENDPOINT}/api/Companies/${company.id}/Members`, {
                FirstName: 'Mai',
                LastName: 'Sakurajima',
                Title: 'Actress',
                Email: 'mai@agency.com',
                Sub: 'auth0|1',
                CompanyId: company.id
            });
        const tm = result.json;

        // Weekly report create
        await frisby
            .post(`${API_ENDPOINT}/api/Companies/${company.id}/Members/${tm.id}/Reports`, {
                //"id": 0,
                "authorId": tm.id,
                //"moraleGradeId": 0,
                "moraleGrade": {
                    //"id": 0,
                    "level": 5,
                    "commentary": "My morale was high!"
                },
                //"stressGradeId": 0,
                "stressGrade": {
                    //"id": 0,
                    "level": 3
                },
                //"workloadGradeId": 0,
                "workloadGrade": {
                    //"id": 0,
                    "level": 4,
                    "commentary": "ok"
                },
                "highThisWeek": "Yay, shot some ad!",
                "lowThisWeek": "Nothing",
                "anythingElse": "I want a tea every day",
                "date": new Date(Date.now()).toISOString()
            })
            .expect('status', 201)
            .expect('jsonTypes', {
                id: Joi.number().required(),
                authorId: Joi.number().required(),
                moraleGradeId: Joi.number().required(),
                stressGradeId: Joi.number().required(),
                workloadGradeId: Joi.number().required(),
                workloadGradeId: Joi.number().required(),
            });

        // Weekly report read
        await frisby
            .get(`${API_ENDPOINT}/api/Companies/${company.id}/Members/${tm.id}/Reports/${report.id}`)
            .expect('status', 200)
            .expect('json', 'id', tm.id)
            .expect('json', 'firstName', tm.firstName)
            .expect('json', 'lastName', tm.lastName)
            .expect('json', 'title', tm.title)
            .expect('json', 'email', tm.email)
            .expect('json', 'sub', tm.sub)
            .expect('json', 'companyId', tm.companyId);

        // Weekly report update
        tm.lastName = "Azusagawa";
        await frisby
            .put(`${API_ENDPOINT}/api/Companies/${company.id}/Members/${tm.id}/Reports/${report.id}`, {
                FirstName: tm.firstName,
                LastName: tm.lastName,
                Title: tm.title,
                Email: tm.email,
                Sub: tm.sub,
                CompanyId: company.id
            })
            .expect('status', 200)
            .expect('json', 'id', tm.id)
            .expect('json', 'lastName', tm.lastName);

        // Weekly report delete
        await frisby
            .delete(`${API_ENDPOINT}/api/Companies/${company.id}/Members/${tm.id}/Reports/${report.id}`)
            .expect('status', 204);

        // Team member clean
        await frisby
            .delete(`${API_ENDPOINT}/api/Companies/${company.id}/Members/${tm.id}`);

        // Company clean
        await frisby
            .delete(`${API_ENDPOINT}/api/Companies/${company.id}`);
    });
});